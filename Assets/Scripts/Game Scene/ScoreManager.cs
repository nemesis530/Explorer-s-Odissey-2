using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; }
    public int HighScore { get; private set; }

    public delegate void ScoreChangedDelegate(int score, int highScore);
    public event ScoreChangedDelegate OnScoreChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            HighScore = DataManager.Instance.HighScore;
            Debug.Log("ScoreManager: Singleton instance assigned.");
        }
        else if (Instance != this)
        {
            Debug.LogWarning("ScoreManager: More than one instance detected, destroying duplicate.");
            Destroy(gameObject);
        }
    }

    public void ResetScore()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score, HighScore);
        Debug.Log("ScoreManager: Score reset.");
    }

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score, HighScore);
        DifficultyManager.Instance.CheckLevelProgression(Score);
        Debug.Log($"ScoreManager: Score increased to {Score}");
    }

    private void UpdateHighScoreIfNeeded()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
            DataManager.Instance.SetHighScore(HighScore);
            Debug.Log($"ScoreManager: New high score set to {HighScore}");
        }
    }

    public void FinalizeScore()
    {
        UpdateHighScoreIfNeeded();
        DataManager.Instance.SetCurrentScore(Score);
        DataManager.Instance.SaveData();
        Debug.Log($"ScoreManager: Finalized score. Current score: {Score}, High score: {HighScore}");
        Debug.Log("ScoreManager: Finalizing score.");
    }

    public void Reset()
    {
        StartCoroutine(ResetAfterDelay(2.5f));
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetScore();
        Debug.Log("ScoreManager resetted after delay.");
    }
}
