using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandsInput_UI : MonoBehaviour
{
    public HandsInput m_input;
    public Text m_left;
    public Text m_right;
    public Image m_eventColor;


    public Color enter = Color.blue;
    public Color down = Color.green;
    public Color up = Color.red;
    public Color exit = Color.cyan;

    private void Start()
    {
        if (m_input.handStates.Length < 2)
            return;
        m_input.handStates[0].m_onEnter.AddListener(ChangeColorEnter);
        m_input.handStates[0].m_onDown.AddListener(ChangeColorDown);
        m_input.handStates[0].m_onUp.AddListener(ChangeColorUp);
        m_input.handStates[0].m_onExit.AddListener(ChangeColorExit);

        m_input.handStates[1].m_onEnter.AddListener(ChangeColorEnter);
        m_input.handStates[1].m_onDown.AddListener(ChangeColorDown);
        m_input.handStates[1].m_onUp.AddListener(ChangeColorUp);
        m_input.handStates[1].m_onExit.AddListener(ChangeColorExit);
    }

    private void ChangeColorExit()
    {
        ChangeColor(exit);
    }

    private void ChangeColorUp()
    {
        ChangeColor(up);
    }

    private void ChangeColorDown()
    {
        ChangeColor(down);
    }

    private void ChangeColorEnter()
    {
        ChangeColor(enter);
    }

    void Update()
    {
        if (m_input.handStates.Length < 2)
            return;
        m_left.text = GetDescription(m_input.handStates[0]);
        m_right.text = GetDescription(m_input.handStates[1]);

     

     
    }
    
    private void ChangeColor(Color color )
    {
        m_eventColor.color = color;
    }
    private void ChangeColor()
    {
        m_eventColor.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return new Color(GetRandomFloat(), GetRandomFloat(), GetRandomFloat(), 1f);
    }

    private float GetRandomFloat()
    {
        return UnityEngine.Random.Range(0, 1);
    }

    private string GetDescription(HandsInput.HandStateRaw state)
    {
        string result = "";
        result += "\n B:Valide: " + state.valid;
        result += "\n UI:ID: " + state.id;
        result += "\n B:isTappingDown: " + state.isTappingDown;
        result += "\n F:tapPRessedTime: " + state.tapPressedStartTime;
        result += "\n V3:StartPos: " + state.latestTapStartPosition;
        result += "\n V3:LastPos: " + state.latestObservedPosition;
        return result;
    }
}
