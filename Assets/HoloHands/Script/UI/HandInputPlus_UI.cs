using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandInputPlus_UI : MonoBehaviour
{
    public HandsInputPlus m_input;
    public Text m_left;
    public Text m_right;

    public Transform m_handLeftDebug;
    public Transform m_handRightDebug;
    public Transform [] m_unknownHandDebug = new Transform[2];
    public Transform m_cameraDirection;

    void Update()
    {
        if (m_input.m_hands.Length < 2)
            return;
        m_left.text = GetDescription(m_input.m_hands[0]);
        m_right.text = GetDescription(m_input.m_hands[1]);
        m_unknownHandDebug[0].position = m_input.m_hands[0].m_lastPositionDetected.m_unityWorldPosition;
        m_unknownHandDebug[1].position = m_input.m_hands[1].m_lastPositionDetected.m_unityWorldPosition;
        m_handLeftDebug.position = m_input.m_left.m_lastPositionDetected.m_unityWorldPosition;
        m_handRightDebug.position = m_input.m_right.m_lastPositionDetected.m_unityWorldPosition;
        if (m_cameraDirection) {
         m_handLeftDebug.forward = m_cameraDirection.forward;
         m_handRightDebug.forward = m_cameraDirection.forward;

        }

       
    }
    private string GetDescription(HandsInputPlus.HandInfo state)
    {
        string result = "";
        result += "\n B:Tracked: " + state.m_isTracked;
        result += "\n B:Tapping: " + state.m_isTapping;
        result += "\n T:Tapping since: " + state.m_timeSinceTapping;
        result += "\n TY:HandType: " + state.GuessHandType();
        result += "\n V3:InitialPosition: " + state.m_firstDetectedPosition.m_unityWorldPosition;
        result += "\n V3:Last Raw: " + state.m_lastPositionDetected.m_rawPosition;
        result += "\n V3:Last World: " + state.m_lastPositionDetected.m_unityWorldPosition;
        result += "\n V3:Last Camera: " + state.m_lastPositionDetected.m_unityCameraLocalPosition;
        return result;
    }
}
