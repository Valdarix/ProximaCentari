using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScene : MonoBehaviour
{
    
    [SerializeField] private AudioClip _buttonSFX;
    public void BackButtonClick()
    {
        AudioManager.Instance._SFXSource.PlayOneShot(_buttonSFX);
        SceneController.Instance.LoadGameScene("MainMenu");    
    }
}
