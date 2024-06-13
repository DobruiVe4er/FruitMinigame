using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; set; }
    [SerializeField] private TextMeshPro scoreText;
    private int score = 0;

    private void Start()
    {
        Instance = this;
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

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}