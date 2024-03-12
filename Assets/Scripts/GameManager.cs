using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//每个玩家一个
public class GameManager : Singleton<GameManager>
{
    public bool IsEditorModeOn;
    public bool EnableWellColliderDetection;
    public bool IsInForest;
    public event Action TransportToForest;
    public event Action TransportToRoom;
    private GameObject _player;

    public void Transport()
    {
        if(!IsInForest)
        {
            IsInForest = true;
            TransportToRoom?.Invoke();
        }
        else
        {
            IsInForest = false;
            TransportToForest?.Invoke();
        }
    }


    public new void Awake()
    {
        base.Awake();
    }

    public void Update()
    {

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }



}
    