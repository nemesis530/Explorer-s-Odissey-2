using TMPro; // Importa lo spazio dei nomi per TextMesh Pro
using UnityEngine;

public class StartGameText : MonoBehaviour
{
    public TextMeshProUGUI startText; // Usa TextMeshProUGUI invece di Text
    public GameController gameController;

    private void OnEnable()
    {
        // Assicurati che il testo sia visibile quando la scena viene caricata
        if (startText != null)
        {
            startText.enabled = true;
        }
    }

    void Update()
    {
        // Controlla se il gioco è iniziato e se il testo non è già nascosto
        if (gameController != null && gameController.IsGameStarted() && startText != null && startText.enabled)
        {
            startText.enabled = false; // Nasconde il testo
        }
    }
}
