using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float maxTimeSpawnRate = 5.0f;
    [SerializeField] private float minTimeSpawnRate = 1.0f;
    [SerializeField] private bool canSpawn = true;

    private GameObject objectInstance;

    void Start()
    {
        StartCoroutine(SpawnerFunction());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator SpawnerFunction()
    {
        float timerSpawn = Random.Range(minTimeSpawnRate, maxTimeSpawnRate);

        yield return new WaitForSeconds(timerSpawn);

        if (canSpawn)
        {
            if (objectInstance == null)
            {
                objectInstance = Instantiate(objectToSpawn, transform.position, transform.rotation);
            }
        }

        StartCoroutine(SpawnerFunction());

    }
}
