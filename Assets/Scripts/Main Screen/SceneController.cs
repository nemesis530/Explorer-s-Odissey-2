using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    // Singleton instance
    public static SceneController Instance { get; private set; }

    [SerializeField]
    private string gameplaySceneName = "GamePlayScene"; // Nome della scena di gioco, assegnalo nell'Editor

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Rendi lo script persistente tra le scene
            Debug.Log("SceneController Instance created and set to persist.");
        }
        else if (Instance != this)
        {
            Debug.LogWarning("Another instance of SceneController found and will be destroyed.");
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Metodo chiamato ogni volta che viene caricata una scena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene {scene.name} loaded.");
        ReassignStartButton();
    }

    private void ReassignStartButton()
    {
        // Trova il pulsante StartButton nella scena corrente e assegna il listener
        Button startButton = GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>();
        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() => LoadScene(gameplaySceneName));
            Debug.Log("StartButton listener reassigned.");
        }
        else
        {
            Debug.LogError("StartButton not found in the scene.");
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
        asyncLoad.allowSceneActivation = false;

        Debug.Log($"Async loading of {sceneName} started.");

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                Debug.Log($"Scene {sceneName} is ready to activate.");
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        Debug.Log($"Scene {sceneName} loaded and activated.");
    }
}
