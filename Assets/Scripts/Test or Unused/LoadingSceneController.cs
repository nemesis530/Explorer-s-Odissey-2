using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    public TMP_Text loadingText;
    public TMP_Text percentageText;
    public static string nextScene;

    void Start()
    {
        Debug.Log("LoadingSceneController: Start invocato.");

        if (loadingText == null || percentageText == null)
        {
            Debug.LogError("LoadingSceneController: I riferimenti al testo UI non sono impostati nell'inspector.");
            return;
        }

        if (string.IsNullOrEmpty(nextScene))
        {
            Debug.LogError("LoadingSceneController: La scena successiva non è stata impostata.");
            return;
        }

        Debug.Log($"LoadingSceneController: La scena '{nextScene}' è stata impostata e verrà caricata.");
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        Debug.Log($"LoadingSceneController: Inizio del caricamento asincrono della scena '{nextScene}'.");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f) * 100;
            percentageText.text = progress.ToString("F2") + "%";
            Debug.Log($"Loading progress: {progress}%");

            if (progress >= 100)
            {
                loadingText.text = "Caricamento Completo";
                Debug.Log("LoadingSceneController: Caricamento completato.");
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(nextScene);
            }

            yield return null;
        }
    }

    public static void LoadScene(string sceneName)
    {
        Debug.Log($"LoadingSceneController: Impostazione di '{sceneName}' come scena successiva.");
        nextScene = sceneName;
    }
}
