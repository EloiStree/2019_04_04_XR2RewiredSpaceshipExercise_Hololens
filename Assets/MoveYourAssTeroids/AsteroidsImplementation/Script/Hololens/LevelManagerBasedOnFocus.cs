using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerBasedOnFocus : MonoBehaviour, IFocusable
{
    public Slider m_levelSlider;
    public int levelToAdd=5;
    public bool m_isFocused;
    public void OnFocusEnter()
    {
        m_isFocused = true;
    }
    public void OnFocusExit()
    {
        m_isFocused = false;
    }
    void Start()
    {
        InvokeRepeating("CheckToAddLevel", 0, 1);
    }
    void CheckToAddLevel()
    {
        if (m_isFocused)
        {
            AddLevel(levelToAdd);

        }

    }
    public  void AddLevel(int levelToAdd)
    {
        m_levelSlider.value += levelToAdd;
    }
}
