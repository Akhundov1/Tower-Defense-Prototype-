using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceManager : MonoBehaviour
{

    public static GameInstanceManager instance = null;

    public Action RefreshUIEvent;
    void Awake()
    {
        if (instance == null)
        { 
            instance = this; 
        }
        else if (instance != this)
        { 
            Destroy(gameObject); 
        }
    }

    public LevelManager levelManager;
    public PlayerManager playerManager;
    public UIManager UserInterfaceManager;

    public void LaunchUIEvent()
    {
        if (RefreshUIEvent != null)
        {
            RefreshUIEvent.Invoke();
        }
    }
}
