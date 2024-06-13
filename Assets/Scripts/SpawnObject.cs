using System.Collections;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    public Sprite replacementSprite; // New field for the replacement sprite as a Sprite
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
        obj.AddComponent<BoxCollider2D>(); // Add a collider for detecting clicks

        // Get the ClickableObject component and pass the replacement sprite as a Sprite
        ClickableObject clickable = obj.AddComponent<ClickableObject>();
        clickable.replacementSprite = replacementSprite;

        // Set the score value: positive for good objects, negative for bad objects
        if (objectIndex == objects.Length - 1)
        {
            clickable.scoreValue = -1; // Negative score for bad objects
        }
        else
        {
            clickable.scoreValue = 1; // Positive score for good objects
        }

        StartCoroutine(SmoothDescent(obj));
    }

    private IEnumerator SmoothDescent(GameObject obj)
    {
        Vector3 start = obj.transform.position;
        Vector3 end = new Vector3(start.x, start.y - 13, start.z);

        float elapsedTime = 0;
        while (elapsedTime < descentSpeed)
        {
            if (obj == null) // Проверяем, существует ли объект
            {
                yield break; // Прерываем корутину, если объект был уничтожен
            }

            obj.transform.position = Vector3.Lerp(start, end, (elapsedTime / descentSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (obj != null) // Еще раз проверяем перед уничтожением
        {
            obj.transform.position = end;
            Destroy(obj);
        }
    }
}