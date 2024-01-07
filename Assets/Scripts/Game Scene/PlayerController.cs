using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initialMoveSpeed = 100f;
    public float maxSpeed = 500f; // Velocità massima
    public GameController gameController;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player.");
            return;
        }
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddMovement();
        }

        CheckBoundsAndBounce();
        LimitSpeed();
    }

    private void AddMovement()
    {
        if (gameController != null && !gameController.IsGameStarted())
        {
            gameController.StartGame();
        }

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 moveDirection = ((Vector2)(worldPoint - transform.position)).normalized;

        rb.velocity += moveDirection * initialMoveSpeed;
    }

    private void CheckBoundsAndBounce()
    {
        float margin = 50f;

        Vector2 minCameraBounds = (Vector2)Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)) + new Vector2(margin, margin);
        Vector2 maxCameraBounds = (Vector2)Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)) - new Vector2(margin, margin);

        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);

        if (playerPosition.x < minCameraBounds.x || playerPosition.x > maxCameraBounds.x)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            transform.position = new Vector2(Mathf.Clamp(playerPosition.x, minCameraBounds.x, maxCameraBounds.x), playerPosition.y);
        }

        if (playerPosition.y < minCameraBounds.y || playerPosition.y > maxCameraBounds.y)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
            transform.position = new Vector2(playerPosition.x, Mathf.Clamp(playerPosition.y, minCameraBounds.y, maxCameraBounds.y));
        }
    }

    private void LimitSpeed()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
