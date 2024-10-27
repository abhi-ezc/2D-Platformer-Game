using Levels;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject levelCompletedUI;
    public GameObject HUD;
    public static GameManager Instance { get; private set; }
    public void Awake ()
    {
        Instance = this;
    }

    public void OnLevelComplete ()
    {
        HUD.SetActive(false);
        levelCompletedUI.SetActive(true);
        LevelManager.Instance.OnLevelComplete();
    }

    public void OnGameOver ()
    {
        HUD.SetActive(false);
        gameOverUI.SetActive(true);
    }
}
