using UnityEngine;

public class JingleManager : MonoBehaviour
{
    public static JingleManager Instance { get; private set; }

    [SerializeField]
    private AudioSource jingleAudioSource;
    [SerializeField]
    private AudioClip[] jingleClips; // Array per le sigle selezionabili
    [SerializeField]
    private AudioClip gameOverClip; // Clip audio dedicato per la GameOverScene

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayJingle(int jingleIndex)
    {
        if (jingleIndex < 0 || jingleIndex >= jingleClips.Length)
        {
            Debug.LogError("Indice della sigla fuori intervallo: " + jingleIndex);
            return;
        }

        AudioClip selectedClip = jingleClips[jingleIndex];
        if
(selectedClip != null)
        {
            jingleAudioSource.clip = selectedClip;
            jingleAudioSource.loop = true;
            jingleAudioSource.Play();
        }
        else
        {
            Debug.LogError("Clip audio non assegnato per l'indice: " + jingleIndex);
        }
    }

    public void PlayGameOverJingle()
    {
        if (gameOverClip != null)
        {
            jingleAudioSource.clip = gameOverClip;
            jingleAudioSource.loop = false; // Potresti voler che non sia in loop per la GameOverScene
            jingleAudioSource.Play();
        }
        else
        {
            Debug.LogError("Clip audio per GameOver non assegnato.");
        }
    }

    public void PlayBaseJingle()
    {
        PlayJingle(DataManager.Instance.SelectedJingleIndex);
    }
}