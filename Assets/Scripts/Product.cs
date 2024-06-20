using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


public enum Producttype { Good, Bad }


public class Product : MonoBehaviour
{
    [SerializeField] private Producttype productType;
    public Sprite replacementSprite;
    private float destroyDelay = 0.5f; 
    public bool hasBeenClicked = false;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
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
            if (productType == Producttype.Good)
            {
                scoreManager.AddScore(1.5f);
            }
            else if (productType == Producttype.Bad)
            {
                scoreManager.SubtractScore(10.5f);
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