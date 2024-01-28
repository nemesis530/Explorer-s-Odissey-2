using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Importazione per TextMeshPro

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
    [SerializeField]
    private GameObject BackgroundOverlay;

    [Header("Settings UI")]
    [SerializeField]
    private Slider jingleVolumeSlider; // Slider per il volume della sigla
    [SerializeField]
    private Slider effectsVolumeSlider; // Slider per il volume degli effetti
    [SerializeField]
    private TMP_Dropdown jingleDropdown;
    [SerializeField]
    private AudioSource jingleAudioSource; // AudioSource per la sigla

    private void Start()
    {
        LoadVolumeSettings();
        InitializeJingleDropdown();
    }

    private void InitializeJingleDropdown()
    {
        jingleDropdown.ClearOptions();
        jingleDropdown.AddOptions(new List<string> { "Sigla 1", "Sigla 2", "Sigla 3" });
        jingleDropdown.value = DataManager.Instance.SelectedJingleIndex;
        jingleDropdown.onValueChanged.AddListener(OnJingleSelectionChanged);
    }

    public void OnStartGameButtonClick()
    {
        StartCoroutine(StartGameWithController());
    }

    private IEnumerator StartGameWithController()
    {
        yield return new WaitForSeconds(1f);
        SceneController.Instance.LoadScene(gamePlaySceneName);
    }

    public void OnSettingsButtonClick()
    {
        settingsPanel.SetActive(true);
    }

    public void OnSaveSettingsButtonClick()
    {
        DataManager.Instance.SetMasterVolume(jingleVolumeSlider.value);
        DataManager.Instance.SetSelectedJingleIndex(jingleDropdown.value);
        DataManager.Instance.SaveData();
        settingsPanel.SetActive(false);
    }

    public void OnExitSettingsButtonClick()
    {
        settingsPanel.SetActive(false);
    }

    public void OnCreditsButtonClick()
    {
        creditsPanel.SetActive(true);
        BackgroundOverlay.SetActive(true);
    }

    public void ResetHighScores()
    {
        DataManager.Instance.ResetHighScores();
    }

    public void OnCloseCreditsButtonClick()
    {
        creditsPanel.SetActive(false);
    }

    public void OnBackgroundClick()
    {
        if (creditsPanel.activeSelf)
        {
            creditsPanel.SetActive(false);
        }
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
        }
        BackgroundOverlay.SetActive(false);
    }

    public void OnJingleSelectionChanged(int selectedIndex)
    {
        DataManager.Instance.SetSelectedJingleIndex(selectedIndex);
        JingleManager.Instance.PlayJingle(selectedIndex);
    }

    public void OnJingleVolumeChanged()
    {
        jingleAudioSource.volume = jingleVolumeSlider.value;
    }

    public void OnEffectsVolumeChanged()
    {
        // Qui dovrai implementare la logica per cambiare il volume degli effetti sonori
        // Dipende da come gestisci gli effetti sonori nel tuo gioco
    }

    private void LoadVolumeSettings()
    {
        float savedVolume = DataManager.Instance.MasterVolume;
        jingleVolumeSlider.value = savedVolume;
        jingleAudioSource.volume = savedVolume;
        // Assicurati di aggiornare anche il volume degli effetti, se necessario
    }

    // Altri metodi e logica...
}
