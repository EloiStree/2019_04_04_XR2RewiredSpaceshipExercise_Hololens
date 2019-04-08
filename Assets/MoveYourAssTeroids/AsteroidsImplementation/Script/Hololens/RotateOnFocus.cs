using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class RotateOnFocus : MonoBehaviour, IFocusable
{
    public Transform m_affectedByRotation;
    public Vector3 m_rotationDirection;
    public float m_rotationSpeed=45f;
    public bool m_isFocused;


    public void OnFocusEnter()
    {
        m_isFocused = true;
    }

    public void OnFocusExit()
    {
        m_isFocused = false;
    }

    
    void Update()
    {
        if (m_isFocused) {
            m_affectedByRotation.Rotate(m_rotationDirection * m_rotationSpeed * Time.deltaTime, Space.Self);
        }
        
    }
    private void Reset()
    {
        m_affectedByRotation = transform;
    }
}
