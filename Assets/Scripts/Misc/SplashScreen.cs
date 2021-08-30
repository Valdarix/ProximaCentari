using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public void SetSplashComplete()
    {
        SceneController.Instance.LoadGameScene("MainMenu");
    }
}
