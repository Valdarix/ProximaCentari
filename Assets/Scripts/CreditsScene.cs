using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScene : MonoBehaviour
{
    public void BackButtonClick()
    {
        SceneController.Instance.LoadGameScene("MainMenu");    
    }
}
