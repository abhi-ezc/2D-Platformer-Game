using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    int score;

    void Start ()
    {
        RefreshUI();
    }
    void RefreshUI ()
    {
        scoreText.SetText($"score : {score}");
    }

    public void IncreaseScore (int increment)
    {
        score += increment;
        RefreshUI();
    }
}
