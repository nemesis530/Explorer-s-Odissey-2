using System.Collections;
using UnityEngine;

public class SurvTimeController : MonoBehaviour
{
    private float startTime;
    private bool timerRunning = false;

    public void StartTiming()
    {
        startTime = Time.time;
        timerRunning = true;
    }

    public void StopTiming()
    {
        if (timerRunning)
        {
            float survivalTime = Time.time - startTime;
            timerRunning = false;

            // Aggiorna il tempo massimo di sopravvivenza se necessario
            DataManager.Instance.CheckAndUpdateMaxSurvivalTime(survivalTime);

            // Aggiorna il tempo di sopravvivenza corrente in DataManager
            DataManager.Instance.SetCurrentSurvivalTime(survivalTime);
        }
    }

    public float GetSurvivalTime()
    {
        return timerRunning ? Time.time - startTime : 0f;
    }

    public float GetMaxSurvivalTime()
    {
        return DataManager.Instance.MaxSurvivalTime;
    }
    // Aggiungi questa coroutine per gestire il reset con ritardo
    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopTiming();
        timerRunning = false; // Assicurati che il timer sia fermato
        Debug.Log("SurvTimeController resetted.");
    }

    // Metodo pubblico per resettare il timer
    public void Reset()
    {
        StartCoroutine(ResetAfterDelay(2f));
    }

}
