using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    // Singleton instance
    public static SceneController Instance { get; private set; }

    public Button startButton; // Assicurati di assegnare questo nel tuo editor Unity

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("SceneController Instance created.");
        }
        else if (Instance != this)
        {
            Debug.LogWarning("Another instance of SceneController found and will be destroyed.");
            Destroy(gameObject);
        }
    }
private void Start()
    {
        // Assicurati che il pulsante sia assegnato dall'editor di Unity.
        if (startButton != null)
        {
            startButton.onClick.AddListener(() => LoadScene("GamePlayScene"));
        }
        else
        {
            Debug.LogWarning("StartButton not assigned in the inspector.");
        }
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log($"Starting to load scene: {sceneName}");
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // Impedisce l'attivazione immediata della scena

        Debug.Log($"Async loading of {sceneName} started.");

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f) // La scena è pronta per essere attivata
            {
                Debug.Log($"Scene {sceneName} is ready to activate.");
                asyncLoad.allowSceneActivation = true; // Permette l'attivazione della scena
            }

            yield return null;
        }

        Debug.Log($"Scene {sceneName} loaded and activated.");
    }

    // Qui puoi aggiungere ulteriori funzionalità come effetti di transizione
}
