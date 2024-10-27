using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        void Awake ()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        void Start ()
        {
            SetFirstLevelStatus();
        }

        void SetFirstLevelStatus ()
        {
            if (GetLevelStatus(1) == LevelStatus.Locked)
            {
                SetLevelStatus(1, LevelStatus.Unlocked);
            }
        }

        public LevelStatus GetLevelStatus (int buildIndex)
        {
            return (LevelStatus)PlayerPrefs.GetInt(buildIndex.ToString(), 0);
        }

        public void SetLevelStatus (int buildIndex, LevelStatus levelStatus)
        {
            PlayerPrefs.SetInt(buildIndex.ToString(), (int)levelStatus);
        }

        public void OnLevelComplete ()
        {
            int currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;
            SetLevelStatus(currentLevelBuildIndex, LevelStatus.Completed);
            Debug.Log(IsValidLevel(currentLevelBuildIndex + 1));

            if (IsValidLevel(currentLevelBuildIndex + 1)
                && GetLevelStatus(currentLevelBuildIndex + 1) == LevelStatus.Locked)
            {
                SetLevelStatus(currentLevelBuildIndex + 1, LevelStatus.Unlocked);
            }
        }

        public void LoadNextLevel ()
        {
            LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LoadLevel (int levelIndex)
        {
            if (levelIndex == 0)
            {
                SceneManager.LoadScene(levelIndex);
            }
            else if (IsValidLevel(levelIndex))
            {
                if (GetLevelStatus(levelIndex) != LevelStatus.Locked)
                {
                    Debug.Log($"Opening Scene level{levelIndex}");
                    Debug.Log($" Current Status : {GetLevelStatus(levelIndex)}");
                    SceneManager.LoadScene(levelIndex);
                }
                else
                {
                    Debug.Log($"Level{levelIndex} is Locked");
                }
            }
            else
            {
                Debug.Log("Invalid level");
            }

        }

        bool IsValidLevel (int buildIndex)
        {
            return buildIndex < SceneManager.sceneCountInBuildSettings;
        }
    }

}
