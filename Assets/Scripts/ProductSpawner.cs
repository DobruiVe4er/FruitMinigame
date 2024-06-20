using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class ProductSpawner : MonoBehaviour
{
    public GameObject[] objects; 
    private float randomX;
    Vector2 whereToSpawn;
    public float spawnDelay;
    private float nextSpawn = 0.0f;
    private int currentIndex = 0;
    private List<GameObject> productsOnScene = new List<GameObject>(); 
    public float ChanceToRespawnGood = 0.9f;

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            if (Time.time > nextSpawn && currentIndex < objects.Length)
            {
                float chance = Random.Range(0f, 1f);
                nextSpawn = Time.time + spawnDelay;
                randomX = Random.Range(transform.position.x - 2f,transform.position.x + 2f);
                whereToSpawn = new Vector2(randomX, transform.position.y);
                if (chance > ChanceToRespawnGood)
                {
                    currentIndex = Random.Range(0, objects.Length - 10);
                }
                else
                {
                    currentIndex = Random.Range(5, objects.Length - 0);
                }
                GameObject product = Instantiate(objects[currentIndex], whereToSpawn, Quaternion.identity);
                productsOnScene.Add(product);
                StartCoroutine(RemoveFromList(product, 4f));
                currentIndex++;
            }
            else if (currentIndex == 15)
            {
                currentIndex = 0;
            }
        }
    }

    private IEnumerator RemoveFromList(GameObject product, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(product);
        productsOnScene.Remove(product); 
    }
}