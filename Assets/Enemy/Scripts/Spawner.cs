using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float maxSpawnRate = 4.0f;
    [SerializeField] private float minSpawnRate = 1.0f;
    [SerializeField] private bool canSpawn = true;

    private GameObject objectInstance;

    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        float spawnTimer = Random.Range(minSpawnRate, maxSpawnRate);

        yield return new WaitForSeconds(spawnTimer);

        if (canSpawn)
        {
            if (objectInstance == null)
            {
                objectInstance = Instantiate(objectToSpawn, transform.position, transform.rotation);
            }
        }

        StartCoroutine(SpawnObject());

    }
}
