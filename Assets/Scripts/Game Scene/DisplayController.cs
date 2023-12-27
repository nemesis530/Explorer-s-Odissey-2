// DisplayController.cs
using TMPro;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assegna questo campo dallo Unity Inspector
    public TextMeshProUGUI levelText; // Assegna questo campo dallo Unity Inspector

    private void Start()
    {
        if (GameController.Instance == null)
        {
            Debug.LogError("DisplayController: GameController.Instance è null.");
            return;
        }

        ScoreManager scoreManager = GameController.Instance.scoreManager;
        DifficultyManager difficultyManager = GameController.Instance.difficultyManager;

        if (scoreManager == null || difficultyManager == null)
        {
            Debug.LogError("DisplayController: Manager non trovati.");
            return;
        }

        scoreManager.OnScoreChanged += UpdateScoreDisplay;
        difficultyManager.OnLevelChanged += UpdateLevelDisplay;

        UpdateScoreDisplay(scoreManager.Score);
        UpdateLevelDisplay(difficultyManager.GetCurrentLevel());
    }

    private void UpdateScoreDisplay(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + newScore.ToString();
        }
        else
        {
            Debug.LogError("DisplayController: scoreText non assegnato.");
        }
    }

    private void UpdateLevelDisplay(int newLevel)
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + newLevel.ToString();
        }
        else
        {
            Debug.LogError("DisplayController: levelText non assegnato.");
        }
    }

    private void OnDestroy()
    {
        if (GameController.Instance != null)
        {
            if (GameController.Instance.scoreManager != null)
            {
                GameController.Instance.scoreManager.OnScoreChanged -= UpdateScoreDisplay;
            }
            if (GameController.Instance.difficultyManager != null)
            {
                GameController.Instance.difficultyManager.OnLevelChanged -= UpdateLevelDisplay;
            }
        }
    }
}
