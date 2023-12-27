using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public Animator playerAnimator; // Assegna l'Animator dell'esplosione nell'Inspector di Unity
    public GameObject explosionObject; // Assegna l'oggetto dell'esplosione nell'Inspector
    public string gameOverSceneName = "GameOverScene"; // Sostituisci con il nome effettivo della tua scena di Game Over
    private SpriteRenderer playerSprite; // Riferimento allo SpriteRenderer del giocatore

    private void Start()
    {
        // Ottieni il componente SpriteRenderer del giocatore
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Log della collisione

        // Ferma il giocatore e impedisce ulteriori movimenti e collisioni
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Debug.Log("Player movement stopped."); // Conferma l'arresto del movimento del giocatore
        }

        // Assicurati che l'oggetto dell'esplosione sia attivo
        if (explosionObject != null)
        {
            explosionObject.SetActive(true);
            Debug.Log("Explosion object activated."); // Conferma l'attivazione dell'oggetto dell'esplosione
        }
        else
        {
            Debug.LogError("Explosion object not assigned in the Inspector.");
        }

        // Imposta l'Animator per l'esplosione e avvia l'animazione
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Explode");
            Debug.Log("Explosion trigger set."); // Conferma l'attivazione del trigger dell'esplosione
        }
        else
        {
            Debug.LogError("Player Animator not assigned in the Inspector.");
        }

        // Avvia le coroutine per gestire la fine dell'animazione e il cambio di scena
        StartCoroutine(HandleAnimationEnd());
    }

    // Coroutine per attendere la fine dell'animazione, disattivare il renderer del giocatore, e poi attendere ulteriori 2 secondi prima di cambiare scena
    IEnumerator HandleAnimationEnd()
    {
        // Calcola la lunghezza dell'animazione di esplosione
        Animator explosionAnimator = explosionObject.GetComponent<Animator>();
        float explosionLength = explosionAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Debug.Log("Explosion animation length: " + explosionLength); // Log della durata dell'animazione

        // Attendi la fine dell'animazione
        yield return new WaitForSeconds(explosionLength);

        // Disattiva il renderer del giocatore per farlo sparire
        if (playerSprite != null)
        {
            playerSprite.enabled = false;
            Debug.Log("Player sprite renderer disabled."); // Conferma la disattivazione dello SpriteRenderer del giocatore
        }

        // Avvia la coroutine per cambiare scena dopo ulteriori 2 secondi
        StartCoroutine(ChangeSceneAfterDelay());
    }

    // Coroutine per attendere ulteriori 2 secondi e poi cambiare scena
    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(1);

        Debug.Log("Loading Game Over Scene."); // Log del caricamento della scena di Game Over
        SceneManager.LoadScene(gameOverSceneName); // Carica la scena di Game Over
    }
}
