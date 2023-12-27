// DifficultyDisplay.cs
using TMPro;
using UnityEngine;

public class DifficultyDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    void Update()
    {
        if (GameController.Instance != null && GameController.Instance.difficultyManager != null)
        {
            // Aggiorna il testo del livello basandoti sul livello corrente da DifficultyManager
            levelText.text = "Level: " + GameController.Instance.difficultyManager.GetCurrentLevel().ToString();
        }
    }
}
