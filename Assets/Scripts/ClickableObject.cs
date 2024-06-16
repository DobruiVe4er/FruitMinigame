using System.Collections;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public Sprite replacementSprite;
    public float destroyDelay = 2f; 
    public int scoreValue = 1; 
    public bool hasBeenClicked = false; 

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene.");
        }
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector2 inputPosition;

            if (Input.GetMouseButtonDown(0))
            {
                inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                inputPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            Collider2D collider = Physics2D.OverlapPoint(inputPosition);

            if (collider != null && collider.gameObject == gameObject)
            {
                OnClick();
            }
        }
    }

    private void OnClick()
    {
        
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null && replacementSprite != null)
        {
            renderer.sprite = replacementSprite;
        }

        hasBeenClicked = true;
        
        if (scoreManager != null)
        {
            if (scoreValue > 0)
            {
                scoreManager.AddScore(scoreValue);
                Debug.Log("Added " + scoreValue + " points. Total score: " + scoreManager.GetScore());
            }
            else
            {
                scoreManager.SubtractScore(-scoreValue);
                Debug.Log("Subtracted " + (-scoreValue) + " points. Total score: " + scoreManager.GetScore());
            }
        }

        
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}