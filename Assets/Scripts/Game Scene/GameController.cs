using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public DifficultyManager difficultyManager;
    public ScoreManager scoreManager;
    public ObstacleSpawnerController obstacleSpawner;
    public GameObject startGameText;

    private bool gameStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameController instance created");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicate GameController instance destroyed");
        }
    }

    private void Update()
    {
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            StartGame();
            Debug.Log("Mouse button pressed - Starting game");
        }
    }

    public void StartGame()
    {
        if (gameStarted)
        {
            Debug.LogWarning("Game already started");
            return;
        }

        gameStarted = true;
        startGameText?.SetActive(false); // Nascondi il testo di inizio gioco se presente

        // Avvia i vari componenti del gioco, controllando che non siano nulli
        obstacleSpawner?.StartSpawning();
        difficultyManager?.StartDifficulty();
        scoreManager?.ResetScore();

        Debug.Log("Gameplay elements initialized and game started");
    }

    public void EndGame()
    {
        obstacleSpawner?.StopSpawning();
        scoreManager?.SaveScore();
        SceneManager.LoadScene("GameOverScene"); // Assicurati di avere una scena di GameOver
        Debug.Log("Game ended, loading Game Over scene");
    }

    public void UpdateObstacleDifficulty(float newSpawnRate, float newObstacleSpeed)
    {
        obstacleSpawner?.UpdateDifficulty(newSpawnRate, newObstacleSpeed);
        Debug.Log($"Obstacle difficulty updated: Spawn Rate = {newSpawnRate}, Speed = {newObstacleSpeed}");
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }

    // Altri metodi se necessari...
}
