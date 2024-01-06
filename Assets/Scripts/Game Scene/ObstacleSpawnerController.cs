using System.Collections;
using UnityEngine;

public class ObstacleSpawnerController : MonoBehaviour, IDifficultyObserver
{
    public GameObject obstacleController; // GameObject che ha lo script Obstacle
    public float verticalRangeMin = -5.0f;
    public float verticalRangeMax = 50.0f;
    private float nextSpawnTime;
    private bool isSpawning = false;
    private Obstacle obstacleScript;

    private void Start()
    {
        DifficultyManager.Instance.RegisterObserver(this);
        if (obstacleController != null)
        {
            obstacleScript = obstacleController.GetComponent<Obstacle>();
            if (obstacleScript == null)
            {
                Debug.LogError("Obstacle script not found on the obstacleController object.");
            }
        }
        else
        {
            Debug.LogError("ObstacleController GameObject is not assigned in the Inspector.");
        }
        Debug.Log("ObstacleSpawnerController started.");
    }

    private void Update()
    {
        if (isSpawning && Time.time >= nextSpawnTime)
        {
            SpawnObstacles();
            nextSpawnTime = Time.time + obstacleScript.GetSpawnRate();
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        nextSpawnTime = Time.time;
        Debug.Log("Spawning enabled.");
    }

    public void StopSpawning()
    {
        isSpawning = false;
        Debug.Log("Spawning disabled.");
    }

    private void SpawnObstacles()
    {
        if (obstacleScript == null) return;

        int currentLevel = DifficultyManager.Instance.GetCurrentLevel();
        Debug.Log($"Current level: {currentLevel}");

        SpawnObstacleType1();

        if (currentLevel >= 10)
        {
            SpawnObstacleType2();
        }

        if (currentLevel >= 20)
        {
            SpawnObstacleType3();
        }
    }

    private void SpawnObstacleType1()
    {
        float spawnY = Random.Range(verticalRangeMin, verticalRangeMax);
        obstacleScript.SpawnObstacle(0, DifficultyManager.Instance.CurrentObstacleSpeed, spawnY);
        Debug.Log("Spawned ObstacleType1 at Y: " + spawnY);
    }

    private void SpawnObstacleType2()
    {
        float spawnY = Random.Range(verticalRangeMin, verticalRangeMax);
        obstacleScript.SpawnObstacle(1, DifficultyManager.Instance.CurrentObstacleSpeed, spawnY);
        Debug.Log("Spawned ObstacleType2 at Y: " + spawnY);
    }

    private void SpawnObstacleType3()
    {
        float spawnY = Random.Range(verticalRangeMin, verticalRangeMax);
        obstacleScript.SpawnObstacle(2, DifficultyManager.Instance.CurrentObstacleSpeed, spawnY);
        Debug.Log("Spawned ObstacleType3 at Y: " + spawnY);
    }

    public void OnDifficultyChanged(float newSpawnRate, float newObstacleSpeed, int currentLevel)
    {
        obstacleScript.SetSpawnRate(newSpawnRate);
        Debug.Log($"Difficulty changed: Spawn Rate = {newSpawnRate}, Speed = {newObstacleSpeed}, Level = {currentLevel}");
    }

    // Aggiungi questa coroutine per gestire il reset con ritardo
    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopSpawning();
        isSpawning = false; // Assicurati che lo spawning sia disattivato
        Debug.Log("ObstacleSpawnerController resetted.");
    }

    // Metodo pubblico per resettare lo spawner
    public void Reset()
    {
        StartCoroutine(ResetAfterDelay(2f));
    }
}
