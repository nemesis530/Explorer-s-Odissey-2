using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initialMoveSpeed = 90f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player.");
            return; // Interrompe ulteriori esecuzioni se rb � null
        }
        rb.gravityScale = 0; // Assicurati che non ci sia gravit� applicata
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddMovement();
        }
    }

    private void AddMovement()
    {
        // Inizia il gioco solo se non � gi� iniziato
        if (!GameController.Instance.IsGameStarted())
        {
            GameController.Instance.StartGame();
        }

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 moveDirection = ((Vector2)(worldPoint - transform.position)).normalized;

        // Aggiunge la velocit� alla velocit� corrente
        rb.velocity += moveDirection * initialMoveSpeed;
    }
}
