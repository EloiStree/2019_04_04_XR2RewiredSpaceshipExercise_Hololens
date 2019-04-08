using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListenToPlayerNeed : MonoBehaviour, ISpeechHandler
{
    public string m_lastSpeechDetected;
    public Text m_lastSpeechDetectedDebug;
    public Slider m_levelWanted;
    public string m_nextLevelName = "SampleScene";


    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        m_lastSpeechDetected = eventData.RecognizedText.ToLower() ;
        m_lastSpeechDetectedDebug.text = m_lastSpeechDetected;

        if ((m_lastSpeechDetected == "restart") || (m_lastSpeechDetected == "start"))
            StartNextLevel();
        if (m_lastSpeechDetected == "level up")
            ChangeLevel(1);
        if (m_lastSpeechDetected == "level down")
            ChangeLevel(-1);
    }

    private void ChangeLevel(int additionalLevel)
    {
        m_levelWanted.value += additionalLevel;
    }

    private void StartNextLevel()
    {
        SceneManager.LoadScene(m_nextLevelName);
    }

}
