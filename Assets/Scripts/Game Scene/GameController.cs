using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour, IDifficultyObserver
{
    public DifficultyManager difficultyManager;
    public ScoreManager scoreManager;
    public ObstacleSpawnerController obstacleSpawner;
    public GameObject player;
    public TerrainMove terrainMove;
    public SurvTimeController survTimeController;
    public DisplayController displayController;

    private bool gameStarted = false;
    private bool isRestarting = false;
    private float startTime;

    private void Start()
    {
        Debug.Log("GameController: Start called.");

        if (difficultyManager != null)
        {
            difficultyManager.RegisterObserver(this);
            Debug.Log("GameController: Registered with DifficultyManager.");
        }
        else
        {
            Debug.LogWarning("GameController: DifficultyManager not found.");
        }
    }

    private void Update()
    {
        if (!gameStarted && !isRestarting && PlayerInputReceived())
        {
            Debug.Log("GameController: Player input received, starting game.");
            StartGame();
        }
    }

    private bool PlayerInputReceived()
    {
        return Input.GetMouseButtonDown(0);
    }

    public void StartGame()
    {
        gameStarted = true;
        startTime = Time.time;

        if (obstacleSpawner != null)
        {
            obstacleSpawner.StartSpawning();
            Debug.Log("GameController: Obstacle spawning started.");
        }
        else
        {
            Debug.LogWarning("GameController: ObstacleSpawner not found.");
        }

        difficultyManager.ResetDifficulty();
        scoreManager.ResetScore();

        if (terrainMove != null)
        {
            terrainMove.enabled = true;
            Debug.Log("GameController: Terrain movement enabled.");
        }
        else
        {
            Debug.LogWarning("GameController: TerrainMove not found.");
        }

        Debug.Log("GameController: Game started.");
    }

    public void EndGame()
    {
        Debug.Log("GameController: Ending game.");
        StartCoroutine(ResetGameStartedAfterDelay(3f));

        obstacleSpawner.StopSpawning();
        Debug.Log("GameController: Obstacle spawning stopped.");

        if (terrainMove != null)
        {
            terrainMove.enabled = false;
            Debug.Log("GameController: Terrain movement disabled.");
        }

        float survivalTime = Time.time - startTime;
        DataManager.Instance.CheckAndUpdateMaxSurvivalTime(survivalTime);
        DataManager.Instance.SetCurrentSurvivalTime(survivalTime);

        // Finalizza il punteggio, aggiorna il punteggio massimo e salva i dati
        scoreManager.FinalizeScore();
        Debug.Log($"GameController: Score saved to DataManager: {scoreManager.Score}");

        Debug.Log("GameController: Game ended.");
    }


    private IEnumerator ResetGameStartedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameStarted = false;
        Debug.Log("GameController: gameStarted reset to false.");
    }

    public void NotifyGameOver()
    {
        Debug.Log("GameController: Game over notified.");
        EndGame();
        ResetGameComponents();
        isRestarting = true;
    }

    private void ResetGameComponents()
    {
        Debug.Log("GameController: Resetting game components.");

        if (obstacleSpawner != null)
        {
            obstacleSpawner.Reset();
            Debug.Log("GameController: ObstacleSpawnerController reset.");
        }

        if (survTimeController != null)
        {
            survTimeController.Reset();
            Debug.Log("GameController: SurvTimeController reset.");
        }

        DifficultyManager.Instance.Reset();
        Debug.Log("GameController: DifficultyManager reset.");

        ScoreManager.Instance.Reset();
        Debug.Log("GameController: ScoreManager reset.");

        if (displayController != null)
        {
            displayController.Reset();
            Debug.Log("GameController: DisplayController reset.");
        }

        Obstacle obstacleInstance = FindObjectOfType<Obstacle>();
        if (obstacleInstance != null)
        {
            obstacleInstance.ResetAll();
            Debug.Log("GameController: Obstacle reset.");
        }
    }

    public void OnDifficultyChanged(float newSpawnRate, float newObstacleSpeed, int newLevel)
    {
        Debug.Log($"GameController: Difficulty updated - Spawn Rate: {newSpawnRate}, Speed: {newObstacleSpeed}, Level: {newLevel}.");
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }

    public float GetSurvivalTime()
    {
        return gameStarted ? Time.time - startTime : 0f;
    }
}
