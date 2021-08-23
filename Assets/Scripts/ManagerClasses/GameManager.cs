
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
        Easy,
        Normal,
        Hard
    }

    private GameDifficulty _currentDifficulty;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }
    
    void Start()
    {
        AudioManager.Instance.PlayClip(0);
    }
    
    public void SetDifficulty(GameDifficulty selectedDifficulty)
    {
        _currentDifficulty = selectedDifficulty;
    }

   
}
