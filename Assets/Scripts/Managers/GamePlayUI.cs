using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GR.Player;
public class GamePlayUI : MonoBehaviour
{
    PlayerController controller;
    public void RegisterPlayer(PlayerController controller)
    {
        this.controller = controller;
    }
    public void OnLeftHold()
    {
        controller.Turn(false);
    }

    public void OnRightHold()
    {
        controller.Turn(true);
    }

    public void OnSpecialUpActive()
    {
        controller.ActiveSpecial(1);
    }

    public void OnSpecialUpDeactive()
    {
        controller.ActiveSpecial(0);
    }

    public void OnSpecialDownActive()
    {
        controller.ActiveSpecial(2);
    }

    public void OnSpecialDownDeactive()
    {
        controller.ActiveSpecial(0);
    }
}
