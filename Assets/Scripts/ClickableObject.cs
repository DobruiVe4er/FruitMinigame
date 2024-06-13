using System.Collections;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public Sprite replacementSprite;
    public float destroyDelay = 2f; // Delay before destroying the object
    public int scoreValue = 1; // Points to add or subtract

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnMouseDown()
    {
        // Replace the sprite of the current object
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null && replacementSprite != null)
        {
            renderer.sprite = replacementSprite;
        }

        // Update the score based on scoreValue
        if (scoreManager != null)
        {
            if (scoreValue > 0)
            {
                scoreManager.AddScore(scoreValue);
            }
            else
            {
                scoreManager.SubtractScore(-scoreValue);
            }
        }

        // Start the destruction process after a delay
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}