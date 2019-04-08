using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewired_Hololens1_HandTracking : MonoBehaviour
{
    public HandsInputPlus m_handInput;

    public string playerId = "Player";
    public string controllerId = "Hololens1";
    private Player player;


    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);

        for (int i = 0; i < player.controllers.customControllerCount; i++)
        {
            CustomController controller = player.controllers.CustomControllers[i];
            // Debug.Log("Controller:"+ controller.hardwareName);
            if (controller.hardwareName == controllerId)
            {
                controller.SetAxisUpdateCallback(GetAxisInfo);
                controller.SetButtonUpdateCallback(GetButtonInfo);
                //   Debug.Log("Hey mon ami");
            }

        }
    }

    protected bool GetButtonInfo(int buttonId)
    {
        switch (buttonId)
        {
            //AnyDrag
            case 0: return IsDragging(false) || IsDragging(true);
            //Left Dragging
            case 2: return IsDragging(false);
            //Left In View
            case 3: return IsInViewDragging(false);
            //Right Dragging
            case 4: return IsDragging(true);
            //Right In View
            case 5: return IsInViewDragging(true);
            default:
                return false;
        }

    }

    private bool IsInViewDragging(bool isRight)
    {
        return isRight ? m_handInput.m_right.m_isTracked : m_handInput.m_left.m_isTracked;
    }

    private bool IsDragging(bool isRight)
    {
       return isRight ? m_handInput.m_right.m_isTapping: m_handInput.m_left.m_isTapping;
    }

    protected float GetAxisInfo(int axisId) {

        Vector3 cameraLeftPos = m_handInput.m_left.m_lastPositionDetected.m_unityCameraLocalPosition;
        Vector3 cameraRightPos = m_handInput.m_left.m_lastPositionDetected.m_unityCameraLocalPosition;
        switch (axisId)
        {
            case 0: return Mathf.Clamp(cameraLeftPos.x * 3f, -1, 1);
            case 1: return Mathf.Clamp(cameraLeftPos.y * 3f, -1, 1);
            case 2: return 0;
            case 3: return Mathf.Clamp(cameraRightPos.x * 3f, -1, 1);
            case 4: return Mathf.Clamp(cameraRightPos.y * 3f, -1, 1);
            case 5: return 0;
            default:
                return 0f;
        }
       
    }

}
