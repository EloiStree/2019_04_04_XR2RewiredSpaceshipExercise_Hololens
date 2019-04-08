using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsInputPlus : MonoBehaviour
{
    [Header("In")]
    public HandsInput m_handsinput;
    public Transform m_cameraRepresentative;
    public Transform m_startPointOfHandHololens;

    [Header("Result")]
    public HandInfo m_left= new HandInfo();
    public HandInfo m_right= new HandInfo();
    public HandInfo[] m_hands = new HandInfo[] { new HandInfo(), new HandInfo() };

    [System.Serializable]
    public class HandInfo
    {
        public bool m_isTracked;
        public bool m_isTapping;
        public HandSideType m_handType;
        public float m_timeSinceTapping;
        public ConvertedPosition m_lastPositionDetected;
        public ConvertedPosition m_startTappingPositionDetected;
        public ConvertedPosition m_firstDetectedPosition;

        public HandSideType GuessHandType()
        {
            if (!m_isTracked)
                return HandSideType.Unknow;
            if (m_firstDetectedPosition.m_unityCameraLocalPosition == Vector3.zero)
                return HandSideType.Unknow;
            return m_firstDetectedPosition.m_unityCameraLocalPosition.x >= 0f ? HandSideType.Right : HandSideType.Left;
        }

        internal void Reset()
        {
            m_isTracked = false;
            m_isTapping = false;
            m_handType = HandSideType.Unknow;
            m_timeSinceTapping = 0;
            m_lastPositionDetected.Reset();
            m_firstDetectedPosition.Reset();
            m_firstDetectedPosition.Reset();
        }
    }
    public struct ConvertedPosition
    {
        public bool HasBeenSet() { return m_rawPosition != Vector3.zero; }

        internal void Reset()
        {
            m_rawPosition = Vector3.zero;
            m_unityWorldPosition = Vector3.zero;
            m_unityCameraLocalPosition = Vector3.zero;
        }

        public Vector3 m_rawPosition;
        public Vector3 m_unityWorldPosition;
        public Vector3 m_unityCameraLocalPosition;

    }
    public enum HandSideType { Unknow, Left, Right }


    public void Update()
    {
        if (m_handsinput.handStates.Length < 2)
            return;

        RefreshDataWith(m_handsinput.handStates[0], 0);
        RefreshDataWith(m_handsinput.handStates[1], 1);


        AddTimeToTapping(0);
        AddTimeToTapping(1);

        if (m_left.GuessHandType() != HandSideType.Left)
            m_left = new HandInfo();
        if (m_right.GuessHandType() != HandSideType.Right)
            m_right = new HandInfo();
    }

    private void AddTimeToTapping(int index)
    {
        if (m_hands[index].m_isTapping)
            m_hands[index].m_timeSinceTapping += Time.deltaTime;
        else m_hands[index].m_timeSinceTapping = 0;
    }

    private void RefreshDataWith(HandsInput.HandStateRaw rawHand, int index)
    {
        bool wasTracked = m_hands[index].m_isTracked, isTracked = rawHand.valid;


        if (rawHand.valid)
        {
            m_hands[index].m_isTracked = rawHand.valid;
            m_hands[index].m_isTapping = rawHand.isTappingDown;
            m_hands[index].m_startTappingPositionDetected = ConvertedRawPositionToUnityPosition(rawHand.latestTapStartPosition);
            m_hands[index].m_lastPositionDetected = ConvertedRawPositionToUnityPosition(rawHand.latestObservedPosition);
            m_hands[index].m_firstDetectedPosition = ConvertedRawPositionToUnityPosition(rawHand.firstObservedPosition);
            m_hands[index].m_handType = m_hands[index].GuessHandType();
            if (m_hands[index].m_handType == HandSideType.Left)
                m_left = m_hands[index];
            if (m_hands[index].m_handType == HandSideType.Right)
                m_right = m_hands[index];
      
        }
        else m_hands[index].Reset();



        if (!wasTracked && isTracked)
        {
            NotifyStartTracking(m_hands[index]);
        }
        if (wasTracked && !isTracked)
        {
            NotifyStopTracking(m_hands[index]);
        }


    }

    private ConvertedPosition ConvertedRawPositionToUnityPosition(Vector3 rawPosition)
    {
        ConvertedPosition result;
        result.m_rawPosition = rawPosition;
        result.m_unityWorldPosition = (m_startPointOfHandHololens.rotation* rawPosition) + m_startPointOfHandHololens.position;
        result.m_unityCameraLocalPosition = Quaternion.Inverse(m_cameraRepresentative.rotation) * (result.m_unityWorldPosition - m_cameraRepresentative.position);
        return result;
    }

    private void NotifyStopTracking(HandInfo handInfo)
    {
       // throw new NotImplementedException();
    }

    private void NotifyStartTracking(HandInfo handInfo)
    {
        //throw new NotImplementedException();
    }
}
