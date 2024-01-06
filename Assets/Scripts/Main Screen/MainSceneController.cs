using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    [Header("Scene Management")]
    [SerializeField]
    private string gamePlaySceneName = "GamePlayScene";

    [Header("UI Panels")]
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private GameObject creditsPanel;
    [SerializeField] private GameObject BackgroundOverlay; // Aggiungi questo


    [Header("Settings UI")]
    [SerializeField]
    private Slider volumeSlider;

    private void Start()
    {
        // Carica il volume salvato dal DataManager all'avvio
        LoadVolumeSettings();
    }

    public void OnStartGameButtonClick()
    {
        // Avvia la coroutine per iniziare il gioco dopo un ritardo
        StartCoroutine(StartGameAfterDelay());
    }

    private IEnumerator StartGameAfterDelay()
    {
        // Attendi 1 secondo prima di caricare la scena di gioco
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(gamePlaySceneName);
    }

    public void OnSettingsButtonClick()
    {
        // Mostra il pannello delle impostazioni
        settingsPanel.SetActive(true);
    }

    public void OnSaveSettingsButtonClick()
    {
        // Salva le impostazioni del volume e nasconde il pannello
        DataManager.Instance.SetMasterVolume(volumeSlider.value);
        DataManager.Instance.SaveData();
        settingsPanel.SetActive(false);
    }

    public void OnExitSettingsButtonClick()
    {
        // Nasconde il pannello delle impostazioni senza salvare
        settingsPanel.SetActive(false);
    }

    // Questo metodo viene chiamato quando il button dei Crediti è premuto
    public void OnCreditsButtonClick()
    {
        creditsPanel.SetActive(true); // Attiva il pannello dei Crediti
        BackgroundOverlay.SetActive(true); // Attiva l'overlay di sfondo
    }
    public void ResetHighScores()
    {
        // Resetta i punteggi alti in DataManager
        DataManager.Instance.ResetHighScores();
    }

    public void OnCloseCreditsButtonClick()
    {
        // Nasconde il pannello dei credits
        creditsPanel.SetActive(false);
    }

    // Questo metodo viene chiamato quando si clicca sul background overlay
    public void OnBackgroundClick()
    {
        // Chiudi tutti i pannelli e l'overlay di sfondo
        if (creditsPanel.activeSelf)
        {
            creditsPanel.SetActive(false);
        }
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
        }

        // Disattiva sempre l'overlay di sfondo indipendentemente dal pannello che era aperto
        BackgroundOverlay.SetActive(false);
    }

    private void LoadVolumeSettings()
    {
        // Carica il volume salvato e aggiorna lo slider e il volume del gioco
        float savedVolume = DataManager.Instance.MasterVolume;
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
    }

    // Altri metodi e logica...
}
