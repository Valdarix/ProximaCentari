
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager script is missing.");
            }
            return _instance;
        }
    }

    public enum GameDifficulty
    {
        InsaneMode, //Unused
        UltraHard, //Unused
        Hard, 
        Normal,
        Easy,
        VeryEasy //Unused
    }

    private bool atCcheckpoint;
    public void SetCheckPoint()
    {
        atCcheckpoint = true;
    }

    private GameDifficulty _currentDifficulty;
    public int EnemiesActiveInCurrentWave { get; set; }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        atCcheckpoint = false;
        SetDifficulty(GameDifficulty.Normal);
    }

    public void StartMusic() => AudioManager.Instance.PlayClip(0);

    public void SetDifficulty(GameDifficulty selectedDifficulty) => _currentDifficulty = selectedDifficulty;

    public GameDifficulty GetCurrentDifficulty() => _currentDifficulty;

    private int score;

    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        UIManager.Instance.UpdateScore(score.ToString());
    }

    private int Wave = 0;

    public int GetWave()
    {
        return Wave;
    }

    public void ResetGame()
    {
        if (!atCcheckpoint)
        {
            SetDifficulty(GameDifficulty.Normal);
            score = 0;
            Wave = 0;
        }
        else
        {
            Wave = 8;
        }

    }

 


}
