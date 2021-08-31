using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("UIManager is empty");
            }
            return _instance;
        }
    }

    private void Awake() => _instance = this;

    [SerializeField] private Text _scoreValue;
    [SerializeField] private Text _lifeforce;
    [SerializeField] private Text _noticeTextTime;
    [SerializeField] private GameObject _noticePanel;
    [SerializeField] private GameObject _pauseMenu;
    
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _replayText;
    [SerializeField] private Text _endGameText;

    [SerializeField] private AudioClip _countDownSFX;
    [SerializeField] private AudioClip _countDownDoneSFX;
    [SerializeField] private Image[] _healthImages;
    [SerializeField] private Image _plasmaPowerGauge;
   
    public void UpdateScore(string score) => _scoreValue.text = score;

    public void UpdateLifeforce(int life)
    {
        var currentHealth = -1;
        foreach (var health in _healthImages)
        { 
            health.gameObject.SetActive(currentHealth < life);
            currentHealth++;
        }
    }

    public void UpdatePlasmaLevel(float plasma)
    {
        _plasmaPowerGauge.fillAmount = plasma;
    }

    public void UpdateNoticeTextTimer(string countdown) => _noticeTextTime.text = countdown;
    
    public void Start()
    {
        Time.timeScale = 1;
        Instance.UpdateScore("0");
        Instance.UpdateLifeforce(0);
        Instance.UpdatePlasmaLevel(0.20f);
    }

    private void Update()
    {
        if (Input.GetButton("Pause"))
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void NextWave() => StartCoroutine(StartWaveTimer((int)GameManager.Instance.GetCurrentDifficulty()));

    private IEnumerator StartWaveTimer(int secondsToWait)
    {
        _noticePanel.SetActive(true);
        const int iterator = 0;
       
        while (secondsToWait > iterator)
        {
            Instance.UpdateNoticeTextTimer(secondsToWait.ToString());
            AudioManager.Instance._SFXSource.PlayOneShot(_countDownSFX);
            yield return new WaitForSeconds(1);
            secondsToWait--;
        }
        AudioManager.Instance._SFXSource.PlayOneShot(_countDownDoneSFX);
        _noticePanel.SetActive(false);
        
    }

    public void MainMenuClick()
    {
        SceneController.Instance.LoadGameScene("MainMenu");
        AudioManager.Instance.PlayClip(0);
    }

    public void QuitGameClick()
    {
        Application.Quit();
        
    }

    public void ReturnToGameClick()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void EndGameText(int condition)
    {
        Time.timeScale = 0;
        switch (condition)
        {
            case 0:
                _endGameText.text = "You Win!";
                _replayText.text = "Play Again?";
                break;
            case 1:
                _endGameText.text = "Game Over!";
                _replayText.text = "Try Again?";
                break;
        }
        _gameOverPanel.SetActive(true);
    }

    public void EndGameButtonClick(int condition)
    {
        switch (condition)
        {
            case 0:
                SceneController.Instance.LoadGameScene("GameScene");
                GameManager.Instance.ResetGame();
               
                Instance.UpdateScore("0");
                Instance.UpdateLifeforce(0);
                Instance.UpdatePlasmaLevel(0.20f);
                Time.timeScale = 1;
                AudioManager.Instance.PlayClip(1);
                break;
            case 1:
                Application.Quit();
                break;
        }
    }
}
