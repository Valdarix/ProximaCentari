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
        Instance.UpdateScore("0");
        Instance.UpdateLifeforce(0);
        Instance.UpdatePlasmaLevel(0.20f);
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
}
