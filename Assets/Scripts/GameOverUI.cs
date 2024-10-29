using Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void OnRestart ()
    {
        LevelManager.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuit ()
    {
        LevelManager.Instance.LoadLevel(0);
    }
}
