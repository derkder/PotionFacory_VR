using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //private GameObject _globalLevelCanvas;

    public bool IsEditorModeOn;
    public bool EnableWellColliderDetection;
    //public event Action TransformToForest;

    [SerializeField]
    private GameObject _player;

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
    