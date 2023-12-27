// ScoreDisplay.cs
using TMPro; // Importa lo spazio dei nomi per TextMesh Pro
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (GameController.Instance != null && GameController.Instance.scoreManager != null)
        {
            scoreText.text = "Score: " + GameController.Instance.scoreManager.Score.ToString(); // Aggiorna il testo del punteggio
        }
    }
}
