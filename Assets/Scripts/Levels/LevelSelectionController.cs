using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Levels
{
    public class LevelSelectionController : MonoBehaviour
    {
        public Button buttonLevel1;
        public Button buttonLevel2;
        public Button buttonLevel3;
        public Button buttonLevel4;
        void Awake ()
        {
            buttonLevel1.onClick.AddListener(() => { LoadScene(LevelConstants.Level1); });
            buttonLevel2.onClick.AddListener(() => { LoadScene(LevelConstants.Level2); });
            buttonLevel2.onClick.AddListener(() => { LoadScene(LevelConstants.Level3); });
            buttonLevel2.onClick.AddListener(() => { LoadScene(LevelConstants.Level4); });
        }

        void LoadScene (int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}
