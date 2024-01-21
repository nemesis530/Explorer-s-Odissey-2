using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public VariableJoystick variableJoystick; // Riferimento al joystick virtuale
    public float moveSpeed = 100f; // Velocità di movimento
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

    private void Update()
    {
        if (gameController != null && !gameController.IsGameStarted() && (variableJoystick.Horizontal != 0 || variableJoystick.Vertical != 0))
        {
            gameController.StartGame();
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 moveDirection = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical);
        rb.velocity += moveDirection * moveSpeed * Time.deltaTime;
        LimitSpeed();
    }

    private void LimitSpeed()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
