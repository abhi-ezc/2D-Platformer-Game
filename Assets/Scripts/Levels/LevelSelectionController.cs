using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    public class LevelSelectionController : MonoBehaviour
    {
        public Button buttonLevel1;
        public Button buttonLevel2;
        public Button buttonLevel3;
        public Button buttonLevel4;
        public Button buttonLevel5;
        void Awake ()
        {
            buttonLevel1.onClick.AddListener(() => { LevelManager.Instance.LoadLevel(LevelConstants.Level1); });
            buttonLevel2.onClick.AddListener(() => { LevelManager.Instance.LoadLevel(LevelConstants.Level2); });
            buttonLevel3.onClick.AddListener(() => { LevelManager.Instance.LoadLevel(LevelConstants.Level3); });
            buttonLevel4.onClick.AddListener(() => { LevelManager.Instance.LoadLevel(LevelConstants.Level4); });
            buttonLevel5.onClick.AddListener(() => { LevelManager.Instance.LoadLevel(LevelConstants.Level5); });
        }
    }
}
