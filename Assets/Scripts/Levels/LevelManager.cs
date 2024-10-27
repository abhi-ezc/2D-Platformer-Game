using Sounds;
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

            if (IsValidLevel(currentLevelBuildIndex + 1)
                && GetLevelStatus(currentLevelBuildIndex + 1) == LevelStatus.Locked)
            {
                SetLevelStatus(currentLevelBuildIndex + 1, LevelStatus.Unlocked);
            }
        }

        public void LoadNextLevel ()
        {
            if (!LoadLevel(SceneManager.GetActiveScene().buildIndex + 1))
            {
                // if no more next level
                // goto Lobby
                LoadLevel(0);
            }
        }

        public bool LoadLevel (int levelIndex)
        {
            if (levelIndex == 0)
            {
                SceneManager.LoadScene(levelIndex);
                return true;
            }
            if (IsValidLevel(levelIndex))
            {
                if (GetLevelStatus(levelIndex) != LevelStatus.Locked)
                {
                    SoundManager.Instance.Play(ESound.ButtonClick);
                    Debug.Log($"Opening Scene level{levelIndex}");
                    Debug.Log($" Current Status : {GetLevelStatus(levelIndex)}");
                    SceneManager.LoadScene(levelIndex);
                    return true;
                }
                Debug.Log($"Level{levelIndex} is Locked");
                return false;
            }
            return false;

        }

        bool IsValidLevel (int buildIndex)
        {
            return buildIndex < SceneManager.sceneCountInBuildSettings;
        }
    }

}
