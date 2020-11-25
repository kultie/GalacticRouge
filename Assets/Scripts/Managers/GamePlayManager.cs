using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapEdge
{
    None, Top, Bottom, Left, Right
}

public class GamePlayManager : MonoBehaviour
{
    static GamePlayManager Instance;   

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }   
}
