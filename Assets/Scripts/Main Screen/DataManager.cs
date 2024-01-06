using UnityEngine;
using System.IO;
using System;

[Serializable]
public class SaveData
{
    public int highScore;
    public float masterVolume;
    public float maxSurvivalTime;
    public int score; // Aggiunta per salvare il punteggio corrente
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public int HighScore { get; private set; }
    public float MasterVolume { get; private set; }
    public float MaxSurvivalTime { get; private set; }
    public float CurrentSurvivalTime { get; private set; }
    public int Score { get; private set; } // Proprietà per il punteggio corrente

    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
            LoadData();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ResetHighScores()
    {
        HighScore = 0;
        MaxSurvivalTime = 0f;
        SaveData();
        Debug.Log("High scores reset to 0");
    }

    public void SaveData()
    {
        SaveData data = new SaveData
        {
            highScore = HighScore,
            masterVolume = MasterVolume,
            maxSurvivalTime = MaxSurvivalTime,
            score = Score // Salvataggio del punteggio corrente
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Data saved");
    }

    public void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.highScore;
            MasterVolume = data.masterVolume;
            MaxSurvivalTime = data.maxSurvivalTime;
            Score = data.score; // Caricamento del punteggio corrente
            Debug.Log("Data loaded");
        }
        else
        {
            HighScore = 0;
            MasterVolume = 1.0f;
            MaxSurvivalTime = 0f;
            Score = 0; // Impostazione del punteggio corrente a 0 se non esiste un file di salvataggio
            Debug.Log("No save file found, default values assigned");
        }
    }

    public void SetHighScore(int highScore)
    {
        if (highScore > HighScore)
        {
            HighScore = highScore;
            SaveData();
            Debug.Log($"High score updated to {HighScore}");
        }
    }

    public void CheckAndUpdateMaxSurvivalTime(float time)
    {
        if (time > MaxSurvivalTime)
        {
            MaxSurvivalTime = time;
            SaveData();
            Debug.Log($"Max survival time updated to {MaxSurvivalTime}");
        }
    }

    public void SetMasterVolume(float volume)
    {
        MasterVolume = volume;
        SaveData();
        Debug.Log($"Master volume updated to {MasterVolume}");
    }

    public void SetCurrentSurvivalTime(float time)
    {
        CurrentSurvivalTime = time;
        Debug.Log($"Current survival time updated to {CurrentSurvivalTime}");
    }

    public void SetCurrentScore(int score)
    {
        Score = score;
        Debug.Log($"Current score updated to {Score}");
    }
}
