using UnityEngine;

public class ObstacleSpawnerController : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float verticalRangeMin = -5.0f;
    public float verticalRangeMax = 50.0f;
    private float nextSpawnTime;
    public float spawnRate = 10.0f; // Ogni 5 secondi
    public bool isSpawning = false; // Controllo dello stato di spawning

    private void Update()
    {
        if (!isSpawning && GameController.Instance.IsGameStarted())
        {
            StartSpawning();
        }

        if (isSpawning && Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        nextSpawnTime = Time.time; // Imposta il tempo per il primo spawn
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private void SpawnObstacle()
    {
        float spawnY = Random.Range(verticalRangeMin, verticalRangeMax);
        // Usa la posizione x del prefab 'Ostacolo1' invece di quella di 'ObstacleSpawnerController'
        Vector3 spawnPosition = new Vector3(obstaclePrefab.transform.position.x, spawnY, 0);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }

    public void UpdateDifficulty(float newSpawnRate, float newObstacleSpeed)
    {
        spawnRate = newSpawnRate;
    }
}
