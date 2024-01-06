using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameController != null && gameController.IsGameStarted())
        {
            // Aggiunge un punto al punteggio solo se il gioco è iniziato
            gameController.scoreManager.AddScore(1);
        }
    }
}
