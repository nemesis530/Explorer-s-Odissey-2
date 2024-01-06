using UnityEngine;

public class MainTerrainMove : MonoBehaviour
{
    public float scrollSpeed = 500f; // Velocità di scrolling modificabile dall'editor
    public float tileSizeZ = 4120f; // Lunghezza effettiva del terreno in unità Unity aggiornata

    private Vector3 startPosition;
    private float scrollPosition = 0f; // Posizione di scroll corrente

    void Start()
    {
        startPosition = transform.position; // Salva la posizione iniziale del terreno
    }

    void Update()
    {
        // Aggiorna la posizione di scroll
        scrollPosition += Time.deltaTime * scrollSpeed;

        // Calcola la nuova posizione per creare un effetto di loop
        float newPosition = Mathf.Repeat(scrollPosition, tileSizeZ);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
