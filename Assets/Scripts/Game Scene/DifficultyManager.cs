using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    private readonly float initialSpawnRate = 10.0f;
    private readonly float spawnRateDecrement = 1.5f;
    private readonly float minimumSpawnRate = 2.5f;
    private readonly float maximumObstacleSpeed = 350f;
    private readonly float obstacleSpeedIncrement = 50f;
    private readonly int scorePerLevel = 10;

    private int currentLevel = 1;
    private float currentSpawnRate;
    public float CurrentObstacleSpeed { get; private set; }
    private List<IDifficultyObserver> observers = new List<IDifficultyObserver>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("DifficultyManager: Singleton instance assigned.");
        }
        else if (Instance != this)
        {
            Debug.LogWarning("DifficultyManager: More than one instance detected, destroying duplicate.");
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        ResetDifficulty();
        Debug.Log("DifficultyManager: Difficulty reset at start.");
    }

    public void RegisterObserver(IDifficultyObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
            Debug.Log($"DifficultyManager: Observer {observer} registered.");
        }
    }

    public void DeregisterObserver(IDifficultyObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
            Debug.Log($"DifficultyManager: Observer {observer} deregistered.");
        }
    }

    public void ResetDifficulty()
    {
        currentLevel = 1;
        currentSpawnRate = initialSpawnRate;
        CurrentObstacleSpeed = GetBaseSpeed();
        NotifyObservers();
        Debug.Log("DifficultyManager: Difficulty reset.");
    }

    public void CheckLevelProgression(int score)
    {
        if (score >= scorePerLevel * currentLevel)
        {
            IncreaseLevel();
            Debug.Log($"DifficultyManager: Level progression checked. Score: {score}");
        }
    }

    private void IncreaseLevel()
    {
        currentLevel++;
        Debug.Log($"DifficultyManager: Level increased to {currentLevel}.");

        if (currentLevel % 5 == 0)
        {
            UpdateDifficulty();
        }
        NotifyObservers();
    }

    private void UpdateDifficulty()
    {
        currentSpawnRate = Mathf.Max(minimumSpawnRate, initialSpawnRate - spawnRateDecrement * (currentLevel / 5));
        CurrentObstacleSpeed = Mathf.Min(maximumObstacleSpeed, GetBaseSpeed() + obstacleSpeedIncrement * (currentLevel / 5));
        NotifyObservers();
        Debug.Log($"DifficultyManager: Difficulty updated. Spawn Rate: {currentSpawnRate}, Obstacle Speed: {CurrentObstacleSpeed}");
    }

    private float GetBaseSpeed()
    {
        return 100.0f;
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnDifficultyChanged(currentSpawnRate, CurrentObstacleSpeed, currentLevel);
            Debug.Log($"DifficultyManager: Notifying observer {observer} of difficulty change.");
        }
    }

    public float GetCurrentSpawnRate()
    {
        return currentSpawnRate;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    // Metodo pubblico per iniziare il processo di reset
    public void Reset()
    {
        StartCoroutine(ResetAfterDelay(2f));
    }

    // Coroutine per gestire il reset con ritardo
    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetDifficulty();
        Debug.Log("DifficultyManager: Reset completed after delay.");
    }
}
