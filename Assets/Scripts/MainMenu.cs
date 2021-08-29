
using UnityEngine;



public class MainMenu : MonoBehaviour
{

    [SerializeField] private AudioClip _buttonSFX;
    [SerializeField] private AudioClip _buttonSelectSFX;
    [SerializeField] private GameObject _difficultyPanel;
    public void LoadNewScene(string scene)
    {
        SceneController.Instance.LoadGameScene(scene);
        AudioManager.Instance._audioSource.PlayOneShot(_buttonSelectSFX);
        if (scene == "GameScene")
        {
            AudioManager.Instance.PlayClip(1);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MenuButtonSFXPlay() => AudioManager.Instance._audioSource.PlayOneShot(_buttonSFX);

    public void DifficultySelectionEnable()
    {
        _difficultyPanel.SetActive(true);
    }

    public void SetDifficultyLevel(int difficulty)
    {
        var selectedDifficulty = difficulty switch
        { //TODO: Match these to the GameManager when you add the other difficulties. 
            0 => GameManager.GameDifficulty.Easy,
            1 => GameManager.GameDifficulty.Normal,
            2 => GameManager.GameDifficulty.Hard,
            _ => GameManager.GameDifficulty.Normal
        };

        GameManager.Instance.SetDifficulty(selectedDifficulty);
        LoadNewScene("GameScene");
    }

   
}
