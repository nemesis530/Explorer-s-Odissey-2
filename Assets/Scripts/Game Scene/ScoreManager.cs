using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }
    public delegate void ScoreChangedDelegate(int newScore);
    public event ScoreChangedDelegate OnScoreChanged;

    private DifficultyManager difficultyManager;

    private void Start()
    {
        difficultyManager = GameController.Instance.difficultyManager;
        if (difficultyManager == null)
        {
            Debug.LogError("ScoreManager: DifficultyManager non trovato.");
            return;
        }

        // Collega il controllo del progresso del livello al cambiamento del punteggio
        OnScoreChanged += difficultyManager.CheckLevelProgression;

        ResetScore();
    }

    public void ResetScore()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
        Debug.Log("Score reset.");
    }

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score);
        Debug.Log($"Score increased: {Score}");
    }

    public void SaveScore()
    {
        // Implementa qui la logica per salvare il punteggio se necessario
        Debug.Log($"Score saved: {Score}");
    }

    private void OnDestroy()
    {
        // Assicurati di rimuovere il listener quando l'oggetto viene distrutto
        if (difficultyManager != null)
        {
            OnScoreChanged -= difficultyManager.CheckLevelProgression;
        }
    }
}
