using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public Animator playerAnimator;
    public GameObject explosionObject;
    public string gameOverSceneName = "GameOverScene";
    private SpriteRenderer playerSprite;
    private bool collisionOccurred = false;
    public GameController gameController;

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se il player ha colpito un bordo e interrompi l'esecuzione del metodo se � cos�
        if (collision.gameObject.CompareTag("Borders")) return;

        // Se la collisione non � con i bordi, e il flag collisionOccurred � gi� vero, interrompi l'esecuzione del metodo
        if (collisionOccurred) return;

        Debug.Log("Collision detected with: " + collision.gameObject.name);
        collisionOccurred = true;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Debug.Log("Player movement stopped.");
        }

        if (explosionObject != null)
        {
            explosionObject.SetActive(true);
            Debug.Log("Explosion object activated.");
        }
        else
        {
            Debug.LogError("Explosion object not assigned in the Inspector.");
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Explode");
            Debug.Log("Explosion trigger set.");
        }
        else
        {
            Debug.LogError("Player Animator not assigned in the Inspector.");
        }

        // Registra il tempo di sopravvivenza corrente
        if (gameController != null)
        {
            float survivalTime = gameController.GetSurvivalTime();
            DataManager.Instance.SetCurrentSurvivalTime(survivalTime);

            // Notifica al GameController l'avvenuta collisione
            gameController.EndGame();
            gameController.NotifyGameOver(); // Assicurati che questo metodo esista in GameController
        }

        StartCoroutine(HandleAnimationEnd());
    }
    IEnumerator HandleAnimationEnd()
    {
        Animator explosionAnimator = explosionObject.GetComponent<Animator>();
        float explosionLength = explosionAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Debug.Log("Explosion animation length: " + explosionLength);

        yield return new WaitForSeconds(explosionLength);

        if (playerSprite != null)
        {
            playerSprite.enabled = false;
            Debug.Log("Player sprite renderer disabled.");
        }

        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(1);

        Debug.Log("Loading Game Over Scene using SceneController.");
        SceneController.Instance.LoadScene(gameOverSceneName);
    }
}
