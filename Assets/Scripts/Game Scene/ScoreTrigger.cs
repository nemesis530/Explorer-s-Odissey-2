// ScoreTrigger.cs
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.scoreManager.AddScore(1); // Aggiunge un punto al punteggio
        }
    }
}
