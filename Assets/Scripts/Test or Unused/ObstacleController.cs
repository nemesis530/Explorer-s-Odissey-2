using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f; // Velocità di movimento dell'ostacolo, modificabile dall'Inspector

    public void Initialize(float speed)
    {
        moveSpeed = speed; // Imposta la velocità dell'ostacolo
    }

    private void Update()
    {
        // Muove l'ostacolo verso sinistra usando la velocità definita.
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        // Distrugge l'ostacolo quando esce dalla visuale della camera.
        Destroy(gameObject);
    }
}
