using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshPro scoreText;
    private float score;

    public void AddScore(float points)
    {
        score += points;
        UpdateScoreText();
    }

    public void SubtractScore(float points)
    {
        score -= points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score " + score;
        }
    }
}