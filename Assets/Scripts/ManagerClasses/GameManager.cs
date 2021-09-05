
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
    public float DifficultyModifier { get; set; }
    public void SetDifficulty(GameDifficulty selectedDifficulty)
    {
        _currentDifficulty = selectedDifficulty;

        DifficultyModifier = selectedDifficulty switch
        {
            GameDifficulty.Easy => 1f,
            GameDifficulty.Normal => 0f,
            GameDifficulty.Hard => -1f,
            _ => DifficultyModifier
        };
    }

    public GameDifficulty GetCurrentDifficulty() => _currentDifficulty;

    private int score;

    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        UIManager.Instance.UpdateScore(score.ToString());
    }

    private int Wave;

    public int GetWave()
    {
        return Wave;
    }
    public void SetWave(int currentWave)
    {
        Wave = currentWave;
    }

    public void ResetGame()
    {
        if (!atCcheckpoint)
        {
            SetDifficulty(_currentDifficulty);
            score = 0;
            Wave = 0;
            Instance.EnemiesActiveInCurrentWave = 0;
          // UIManager.Instance.NextWave();
        }
        else
        {
            Wave = 8;
            Instance.EnemiesActiveInCurrentWave = 0;
        }

    }

    private bool lastPhase;

    public void SetLastPhase()
    {
        lastPhase = true;
    }

    public bool GetLastPhase()
    {
        return lastPhase;
    }




}
