using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionOnLongFocus : MonoBehaviour, IFocusable
{
    public UnityEvent  m_toDoWhenFocused;
    public float m_triggerTime=2f;
    [Header("Debug")]
    public bool  m_isFocused;
    public float m_currentFocusedTime;
    public bool  m_hasBeenTriggered;

    public void OnFocusEnter() { m_isFocused = true;   }
    public void OnFocusExit(){ m_isFocused = false;   }

    void Update()
    {
        if (m_isFocused)
        {
            m_currentFocusedTime += Time.deltaTime;
            if (!m_hasBeenTriggered && m_currentFocusedTime > m_triggerTime)
            {
                m_hasBeenTriggered = true;
                m_toDoWhenFocused.Invoke();
            }
        }
        else {
            m_currentFocusedTime=0f;
            m_hasBeenTriggered=false;
        }
    }
}
