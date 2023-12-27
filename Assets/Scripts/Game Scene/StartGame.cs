// StartGame.cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private string gamePlaySceneName = "GamePlayScene";

    public void OnStartButtonClick()
    {
        Debug.Log("StartGame: Pulsante Start Game premuto.");
        StartCoroutine(LoadGamePlaySceneWithDelay());
    }

    private IEnumerator LoadGamePlaySceneWithDelay()
    {
        Debug.Log($"StartGame: In attesa di 0.5 secondi prima del caricamento di '{gamePlaySceneName}'.");
        yield return new WaitForSeconds(0.5f);

        // Aggiungi qui la chiamata per resettare il punteggio
        GameController.Instance.scoreManager.ResetScore();

        Debug.Log($"StartGame: Caricamento della scena '{gamePlaySceneName}'.");
        SceneManager.LoadScene(gamePlaySceneName);
    }
}
