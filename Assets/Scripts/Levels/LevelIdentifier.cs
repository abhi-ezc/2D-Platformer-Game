using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelIdentifier : MonoBehaviour
    {
        TextMeshProUGUI text;

        void Start ()
        {
            text = GetComponent<TextMeshProUGUI>();
            text.SetText(SceneManager.GetActiveScene().name);
            Debug.Log(SceneManager.GetActiveScene().name);
        }
    }
}
