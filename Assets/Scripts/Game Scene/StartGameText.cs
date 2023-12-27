using TMPro; // Importa lo spazio dei nomi per TextMesh Pro
using UnityEngine;

public class StartGameText : MonoBehaviour
{
    public TextMeshProUGUI startText; // Usa TextMeshProUGUI invece di Text

    void Update()
    {
        // Controlla se il gioco è iniziato e se il testo non è già nascosto
        if (GameController.Instance.IsGameStarted() && startText != null && startText.enabled)
        {
            startText.enabled = false; // Nasconde il testo
        }
    }
}
