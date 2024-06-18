using System.Collections;
using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    public GameObject[] objects;
    public float ChanceToRespawnGood = 0.9f;
    public float descentSpeed = 5f;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;

    private void Start()
    {
        StartCoroutine(SpawnRandomObject());
    }

    private IEnumerator SpawnRandomObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            InstantiateRandomObject();
        }
    }

    private void InstantiateRandomObject()
    {
        int objectIndex;
        float chance = Random.Range(0f, 1f);
        if (chance < ChanceToRespawnGood)
        {
            objectIndex = Random.Range(0, objects.Length - 1);
        }
        else
        {
            objectIndex = objects.Length - 1;
        }

        GameObject obj = Instantiate(objects[objectIndex], transform.position, objects[objectIndex].transform.rotation);
        obj.AddComponent<BoxCollider2D>(); 
        ProductClicked clickable = obj.AddComponent<ProductClicked>();
        StartCoroutine(SmoothDescent(obj));
    }

    private IEnumerator SmoothDescent(GameObject obj)
    {
        Vector3 start = obj.transform.position;
        Vector3 end = new Vector3(start.x, start.y - 13, start.z);

        float elapsedTime = 0;
        ProductClicked clickable = obj.GetComponent<ProductClicked>();
        while (elapsedTime < descentSpeed)
        {
            if (obj == null || (clickable != null && clickable.hasBeenClicked)) 
            {
                yield break; 
            }

            obj.transform.position = Vector3.Lerp(start, end, (elapsedTime / descentSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (obj != null) 
        {
            obj.transform.position = end;
            Destroy(obj);
        }
    }
}