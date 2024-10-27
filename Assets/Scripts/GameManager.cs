using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverUI;
    public GameObject levelCompletedUI;
    public GameObject HUD;
    public void Awake ()
    {
        instance = this;
    }

    public void OnLevelComplete ()
    {
        HUD.SetActive(false);
        levelCompletedUI.SetActive(true);
    }

    public void OnGameOver ()
    {
        HUD.SetActive(false);
        gameOverUI.SetActive(true);
    }
}
