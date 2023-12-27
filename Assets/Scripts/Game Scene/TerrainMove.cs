using UnityEngine;

public class TerrainMove : MonoBehaviour
{
    public float scrollSpeed = 150f; // Velocità di scrolling modificabile dall'editor
    public float tileSizeZ = 3930f; // Lunghezza effettiva del terreno in unità Unity

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
