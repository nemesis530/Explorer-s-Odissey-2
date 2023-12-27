// OutOfBoundsDestroyer.cs
using UnityEngine;

public class OutOfBoundsDestroyer : MonoBehaviour
{
    private Camera mainCamera;

    public void SetCamera(Camera camera)
    {
        mainCamera = camera;
    }

    private void Update()
    {
        if (mainCamera != null && !IsInView(mainCamera, transform.position))
        {
            Destroy(gameObject); // Distrugge l'ostacolo se non è più nella visuale della camera
        }
    }

    // Verifica se il punto è nella visuale della camera
    private bool IsInView(Camera camera, Vector3 position)
    {
        Vector3 viewportPoint = camera.WorldToViewportPoint(position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
               viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }
}
