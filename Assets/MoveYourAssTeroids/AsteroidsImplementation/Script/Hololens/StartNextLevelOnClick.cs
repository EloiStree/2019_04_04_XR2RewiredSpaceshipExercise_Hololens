using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNextLevelOnClick : MonoBehaviour, IInputHandler
{
    public string m_nextLevelName="SampleScene";
    public bool m_nextSceneTriggered;

    public void OnInputDown(InputEventData eventData)
    {
        m_nextSceneTriggered = true;
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(m_nextLevelName);
    }

    public void OnInputUp(InputEventData eventData)
    {
    }

}
