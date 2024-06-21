using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Producttype { Good, Bad }

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Producttype productType;
    public TextMeshPro scoreText;
    private float score;

    public void AddScore(float points)
    {
        ChangeScore();
        score += points;
        UpdateScoreText();
    }

    public void SubtractScore(float points)
    {
        ChangeScore();
        score -= points;
        UpdateScoreText();
    }

    public void ChangeScore()
    {
            if (productType == Producttype.Good)
            {
                AddScore(1.5f);
            }
            else if (productType == Producttype.Bad)
            {
                SubtractScore(10.5f);
            }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score " + score;
        }
    }
    
    
}