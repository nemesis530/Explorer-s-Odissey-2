using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI maxSurvivalTimeText;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (DataManager.Instance != null)
        {
            // Assumi che DataManager abbia i campi Score e HighScore
            scoreText.text = "Score: " + DataManager.Instance.Score.ToString();
            highScoreText.text = "High Score: " + DataManager.Instance.HighScore.ToString();

            survivalTimeText.text = "Survival Time: " + DataManager.Instance.CurrentSurvivalTime.ToString("F2") + "s";
            maxSurvivalTimeText.text = "Max Survival Time: " + DataManager.Instance.MaxSurvivalTime.ToString("F2") + "s";
        }
        else
        {
            Debug.LogError("GameOverController: DataManager.Instance è null.");
        }
    }

    public void ReturnToMainMenu()
    {
        SceneController.Instance.LoadScene("MainGameScene");
    }

    public void RestartGame()
    {
        SceneController.Instance.LoadScene("GamePlayScene");
    }
}
