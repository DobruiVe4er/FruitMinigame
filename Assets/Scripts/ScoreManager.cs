using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshPro scoreText;
    private int score = 0;

    private void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Score Text not set in the ScoreManager.");
        }
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void SubtractScore(int points)
    {
        score -= points;
        UpdateScoreText();
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score " + score.ToString();
        }
        else
        {
            Debug.LogError("Score Text not set in the ScoreManager.");
        }
    }
}