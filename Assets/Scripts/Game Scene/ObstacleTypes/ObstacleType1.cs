using System;
using UnityEngine;

public class ObstacleType1 : Obstacle
{
    [SerializeField]
    private float speed = 100.0f;

    private DifficultyManager difficultyManager;

    private void Start()
    {
        // Trova DifficultyManager e registrati
        difficultyManager = FindObjectOfType<DifficultyManager>();
        if (difficultyManager != null)
        {
            difficultyManager.RegisterObstacle(this);
        }
    }

    private void OnDestroy()
    {
        // Deregistra quando l'ostacolo viene distrutto
        if (difficultyManager != null)
        {
            difficultyManager.DeregisterObstacle(this);
        }
    }

    public void UpdateSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    protected override void Update()
    {
        base.Update();
        if (gameObject.name.Contains("Clone"))
        {
            MoveObstacle();
        }
    }

    protected void MoveObstacle()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    internal static float GetBaseSpeed()
    {
        // Restituisci un valore fisso o calcola la velocità base
        return 100.0f; // Sostituisci con il valore appropriato per la tua logica di gioco
    }
}
