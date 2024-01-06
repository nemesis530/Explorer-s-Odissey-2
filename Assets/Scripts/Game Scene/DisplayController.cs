using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayController : MonoBehaviour, IDifficultyObserver
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI maxSurvivalTimeText;
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("DisplayController: GameController not found.");
        }

        ScoreManager scoreManager = ScoreManager.Instance;
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged += UpdateScoreDisplay;
            UpdateScoreDisplay(scoreManager.Score, scoreManager.HighScore);
            Debug.Log("DisplayController: ScoreManager found and subscribed.");
        }
        else
        {
            Debug.LogError("DisplayController: ScoreManager not found.");
        }

        DifficultyManager difficultyManager = DifficultyManager.Instance;
        if (difficultyManager != null)
        {
            difficultyManager.RegisterObserver(this);
            UpdateLevelDisplay(difficultyManager.GetCurrentLevel());
            Debug.Log("DisplayController: DifficultyManager found and subscribed.");
        }
        else
        {
            Debug.LogError("DisplayController: DifficultyManager not found.");
        }

        if (DataManager.Instance != null)
        {
            UpdateMaxSurvivalTimeDisplay(DataManager.Instance.MaxSurvivalTime);
            Debug.Log("DisplayController: DataManager found and max survival time updated.");
        }
    }


    public void OnDifficultyChanged(float newSpawnRate, float newObstacleSpeed, int newLevel)
    {
        UpdateLevelDisplay(newLevel);
    }

    private void UpdateScoreDisplay(int score, int highScore)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }

    private void UpdateLevelDisplay(int newLevel)
    {
        if (levelText != null)
            levelText.text = "Level: " + newLevel;
    }

    private void Update()
    {
        if (gameController != null && gameController.IsGameStarted())
        {
            UpdateSurvivalTimeDisplay(gameController.GetSurvivalTime());
        }

        if (DataManager.Instance != null)
        {
            UpdateMaxSurvivalTimeDisplay(DataManager.Instance.MaxSurvivalTime);
        }
    }

    public void UpdateSurvivalTimeDisplay(float survivalTime)
    {
        if (survivalTimeText != null)
            survivalTimeText.text = "Time: " + survivalTime.ToString("F1") + "s";
    }

    public void UpdateMaxSurvivalTimeDisplay(float maxSurvivalTime)
    {
        if (maxSurvivalTimeText != null)
            maxSurvivalTimeText.text = "Max Time: " + maxSurvivalTime.ToString("F1") + "s";
    }
    // Coroutine per gestire il reset con ritardo
    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetDisplay();
        Debug.Log("DisplayController resetted after delay.");
    }

    // Metodo pubblico per iniziare il processo di reset
    public void Reset()
    {
        StartCoroutine(ResetAfterDelay(2f));
    }

    // Metodo per resettare effettivamente il display
    private void ResetDisplay()
    {
        UpdateScoreDisplay(0, DataManager.Instance.HighScore);
        UpdateLevelDisplay(0);
        UpdateSurvivalTimeDisplay(0);
        UpdateMaxSurvivalTimeDisplay(DataManager.Instance.MaxSurvivalTime);
    }

    private void OnDestroy()
    {
        ScoreManager scoreManager = ScoreManager.Instance;
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged -= UpdateScoreDisplay;
        }

        DifficultyManager difficultyManager = DifficultyManager.Instance;
        if (difficultyManager != null)
        {
            difficultyManager.DeregisterObserver(this);
        }
    }
}
