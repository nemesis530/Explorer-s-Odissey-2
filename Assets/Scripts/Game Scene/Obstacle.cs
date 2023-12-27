using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected bool hasEnteredCameraView = false;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    protected virtual void Update()
    {
        // La logica di movimento specifica sarà nelle classi derivate (come ObstacleType1)
    }

    private void OnBecameInvisible()
    {
        // Distruggi solo gli ostacoli clonati quando escono dalla visuale della camera
        if (hasEnteredCameraView && gameObject.name.Contains("Clone"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameVisible()
    {
        // Segna l'ostacolo come visto dalla camera la prima volta che diventa visibile
        hasEnteredCameraView = true;
    }
}
