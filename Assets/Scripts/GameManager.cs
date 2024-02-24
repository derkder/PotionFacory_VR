using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //场景名称列表
    private List<string> levelScenes = new List<string> {
        "Tutorial_0",
        "Tutorial_1",
        "Tutorial_2",
        "Level_1",
        "Level_2",
        "Level_3",
        "Level_4",
        "Level_5",
        "Level_6",
        "Level_7"
    };

    public int levelProgress = -1;

    private GameObject _globalLevelCanvas;

    public bool isEditorModeOn;
    public bool enableWellColliderDetection;

    public event Action OnLevelPass;

    public new void Awake()
    {
        base.Awake();
    }

    public void Update()
    {

    }


    private void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
    