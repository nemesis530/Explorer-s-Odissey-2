// IDifficultyObserver.cs
public interface IDifficultyObserver
{
    void OnDifficultyChanged(float newSpawnRate, float newObstacleSpeed, int newLevel);
}
