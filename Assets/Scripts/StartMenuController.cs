using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public GameObject levelSelectionUI;

    void Awake ()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void PlayGame ()
    {
        levelSelectionUI.SetActive(true);
    }

    void QuitGame ()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
