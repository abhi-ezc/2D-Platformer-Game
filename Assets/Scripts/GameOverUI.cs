using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void OnRestart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneUnloadCompleted (AsyncOperation asyncOperation)
    {
    }

    public void OnQuit ()
    {
        SceneManager.LoadScene(0);
    }
}
