using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float moveSpeed = 150f;
    private IMovementBehavior movementBehavior;

    public delegate void ObstaclePassed();
    public event ObstaclePassed OnPassed;

    void Start()
    {
        movementBehavior = new StraightMovement();
    }

    void Update()
    {
        movementBehavior.Move(transform, moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPassed?.Invoke();
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}

public interface IMovementBehavior
{
    void Move(Transform obstacleTransform, float speed);
}

public class StraightMovement : IMovementBehavior
{
    public void Move(Transform obstacleTransform, float speed)
    {
        obstacleTransform.position += Vector3.left * speed * Time.deltaTime;
    }
}
