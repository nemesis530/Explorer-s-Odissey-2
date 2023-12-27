using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public delegate void SpeedUpdateDelegate(float newSpeed);
    public event SpeedUpdateDelegate OnSpeedUpdate;

    public delegate void LevelChangeDelegate(int newLevel);
    public event LevelChangeDelegate OnLevelChanged;

    public float initialSpawnRate = 10.0f;
    public float spawnRateDecrement = 1.5f;
    public float minimumSpawnRate = 2.5f;
    public float maximumObstacleSpeed = 350f;
    public float obstacleSpeedIncrement = 50f;

    private int currentLevel = 1;
    private int scorePerLevel = 10;
    private float currentObstacleSpeed; // Velocità corrente degli ostacoli
    private float currentSpawnRate; // Tasso di generazione corrente degli ostacoli

    void Start()
    {
        currentSpawnRate = initialSpawnRate; // Imposta il tasso di generazione iniziale al valore di partenza
        currentObstacleSpeed = ObstacleType1.GetBaseSpeed(); // Imposta la velocità iniziale degli ostacoli
        Debug.Log($"DifficultyManager started. Initial spawn rate: {currentSpawnRate}, Initial obstacle speed: {currentObstacleSpeed}");
    }

    public void StartDifficulty()
    {
        currentLevel = 1;
        scorePerLevel = 10;
        currentSpawnRate = initialSpawnRate; // Reset dello spawn rate al valore iniziale
        currentObstacleSpeed = ObstacleType1.GetBaseSpeed(); // Reset della velocità degli ostacoli al valore base
        OnLevelChanged?.Invoke(currentLevel);
        Debug.Log($"Difficulty started. Level: {currentLevel}, Spawn Rate: {currentSpawnRate}, Speed: {currentObstacleSpeed}");
    }

    public void CheckLevelProgression(int score)
    {
        if (score >= scorePerLevel * currentLevel)
        {
            IncreaseLevel();
        }
    }

    private void IncreaseLevel()
    {
        currentLevel++;
        OnLevelChanged?.Invoke(currentLevel);
        Debug.Log($"Level increased to: {currentLevel}");

        if (currentLevel % 5 == 0)
        {
            UpdateDifficulty();
        }
    }

    private void UpdateDifficulty()
    {
        // Calcola il numero di volte che il decremento di spawn rate deve essere applicato.
        // Questo dovrebbe essere 0 fino al livello 4 e poi aumentare di 1 ogni 5 livelli.
        int levelFactor = Mathf.Max(0, (currentLevel - 1) / 5);

        // Applica il decremento di spawn rate basato su levelFactor.
        currentSpawnRate = Mathf.Max(minimumSpawnRate, initialSpawnRate - (spawnRateDecrement * levelFactor));

        // Applica l'incremento della velocità degli ostacoli se il livello è un multiplo di 5.
        if (currentLevel % 5 == 0)
        {
            currentObstacleSpeed = Mathf.Min(maximumObstacleSpeed, currentObstacleSpeed + obstacleSpeedIncrement);
        }

        // Assicurati che l'evento venga invocato con la velocità aggiornata.
        OnSpeedUpdate?.Invoke(currentObstacleSpeed);

        // Aggiorna le componenti del gioco con i nuovi valori.
        GameController.Instance.UpdateObstacleDifficulty(currentSpawnRate, currentObstacleSpeed);

        // Log per il debug.
        Debug.Log($"[DifficultyManager] Difficulty updated - Level: {currentLevel}, New spawn rate: {currentSpawnRate}, New obstacle speed: {currentObstacleSpeed}");
    }
    public void RegisterObstacle(ObstacleType1 obstacle)
    {
        OnSpeedUpdate += obstacle.UpdateSpeed;
    }

    public void DeregisterObstacle(ObstacleType1 obstacle)
    {
        OnSpeedUpdate -= obstacle.UpdateSpeed;
    }

    public float GetCurrentObstacleSpeed()
    {
        return currentObstacleSpeed;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
