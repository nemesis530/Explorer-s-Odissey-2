using UnityEngine;
using UnityEngine.SceneManagement;

// Questo script fornisce la funzionalità per tornare alla scena del menù principale.
public class ReturnMainScene : MonoBehaviour
{
    // Il nome della scena principale da caricare.
    // Assicurati che questo corrisponda al nome esatto della scena nel tuo progetto Unity.
    [SerializeField] private string mainSceneName = "MainGameScene";

    // Metodo pubblico per ricaricare la scena del menù principale.
    public void LoadMainScene()
    {
        // Stampa un messaggio di log per confermare l'azione.
        Debug.Log("Returning to the main scene: " + mainSceneName);

        // Ricarica la scena principale usando il nome fornito.
        SceneManager.LoadScene(mainSceneName);
    }
}
