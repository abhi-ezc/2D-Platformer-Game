using Levels;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedUI : MonoBehaviour
{

    public Button nextLevelButton;
    public Button quitButton;

    public void Start ()
    {
        nextLevelButton.onClick.AddListener(onNextLevel);
        quitButton.onClick.AddListener(OnQuit);
    }
    void onNextLevel ()
    {
        LevelManager.Instance.LoadNextLevel();
    }

    void OnQuit ()
    {
        LevelManager.Instance.LoadLevel(0);
    }
}
