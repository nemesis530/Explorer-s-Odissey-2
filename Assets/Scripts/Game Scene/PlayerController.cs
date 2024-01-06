using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initialMoveSpeed = 90f;
    public GameController gameController;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player.");
            return; // Interrompe ulteriori esecuzioni se rb è null
        }
        rb.gravityScale = 0; // Assicurati che non ci sia gravità applicata
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
        if (gameController != null && !gameController.IsGameStarted())
        {
            gameController.StartGame();
        }

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 moveDirection = ((Vector2)(worldPoint - transform.position)).normalized;

        // Aggiunge la velocità alla velocità corrente
        rb.velocity += moveDirection * initialMoveSpeed;
    }
}
