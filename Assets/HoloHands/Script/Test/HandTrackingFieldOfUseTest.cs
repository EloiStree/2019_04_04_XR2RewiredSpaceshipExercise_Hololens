using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrackingFieldOfUseTest : MonoBehaviour
{
    public HandsInputPlus m_input;
    public GameObject m_prefab;
    public Transform m_whereToCreateIt;
    void Start()
    {
        InvokeRepeating("CreatePointAtCameraRelativePosition", 0, 1f);
    }

    void CreatePointAtCameraRelativePosition() {

        if (m_input.m_hands[0].m_isTracked) {
            GameObject.Instantiate(m_prefab, m_input.m_hands[0].m_lastPositionDetected.m_unityCameraLocalPosition + m_whereToCreateIt.position, Quaternion.identity);
        }

    }

}
