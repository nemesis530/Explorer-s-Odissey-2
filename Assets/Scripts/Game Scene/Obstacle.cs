using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs; // Array di prefabbricati per i diversi tipi di ostacoli
    [SerializeField] private float baseSpeed = 100.0f; // Velocità base degli ostacoli

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        Debug.Log("Obstacle: Start called, main camera set.");
    }

    public void SpawnObstacle(int obstacleType, float newSpeed, float spawnY)
    {
        if (obstacleType >= 0 && obstacleType < obstaclePrefabs.Length)
        {
            Vector3 spawnPosition = new Vector3(obstaclePrefabs[obstacleType].transform.position.x, spawnY, obstaclePrefabs[obstacleType].transform.position.z);
            GameObject obstacle = Instantiate(obstaclePrefabs[obstacleType], spawnPosition, Quaternion.identity);
            Obstacle obstacleScript = obstacle.GetComponent<Obstacle>();
            if (obstacleScript != null)
            {
                obstacleScript.SetSpeed(newSpeed);
                Debug.Log($"Obstacle spawned of type {obstacleType} with speed {newSpeed}.");
            }
        }
        else
        {
            Debug.LogWarning($"Invalid obstacle type: {obstacleType}. Cannot spawn obstacle.");
        }
    }

    public void SetSpeed(float newSpeed)
    {
        baseSpeed = newSpeed;
        Debug.Log($"Obstacle speed set to {newSpeed}.");
    }

    protected virtual void Update()
    {
        if (gameObject.name.Contains("Clone"))
        {
            MoveObstacle();
        }
    }

    private void MoveObstacle()
    {
        transform.Translate(Vector2.left * baseSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.name.Contains("Clone"))
        {
            Destroy(gameObject);
            Debug.Log("Obstacle destroyed after becoming invisible.");
        }
    }

    public float GetSpawnRate()
    {
        return baseSpeed;
    }

    public void SetSpawnRate(float newSpawnRate)
    {
        baseSpeed = newSpawnRate;
        Debug.Log($"Spawn rate set to {newSpawnRate}.");
    }

    // Metodo pubblico per iniziare il processo di reset
    public void ResetAll()
    {
        StartCoroutine(ResetAllAfterDelay(2.5f));
    }

    // Coroutine per gestire il reset con ritardo
    private IEnumerator ResetAllAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (var obstacle in FindObjectsOfType<Obstacle>())
        {
            if (obstacle.gameObject.name.Contains("Clone"))
            {
                Destroy(obstacle.gameObject);
                Debug.Log("Obstacle resetted.");
            }
        }
        Debug.Log("All obstacles resetted after delay.");
    }
}
