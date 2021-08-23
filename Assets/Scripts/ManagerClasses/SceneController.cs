using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    private static SceneController _instance;

    public static SceneController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Scene Controller is null");
            }

            return _instance;
        }
    }
    
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SceneManager.LoadSceneAsync("MainMenu");

    }

    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        
    }
}
