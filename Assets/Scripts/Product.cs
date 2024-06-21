using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Product : MonoBehaviour
{
    float destroyDelay = 0.5f; 
    private bool hasBeenClicked = false;
    public Sprite replacementSprite;
    
    
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

    public void OnClick()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null && replacementSprite != null)
        {
            renderer.sprite = replacementSprite;
        }

        hasBeenClicked = true;
            
        StartCoroutine(DestroyAfterDelay());
    }
    
    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}