using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;

    [SerializeField] GameObject performances;

    // Slider pour le volume principal et le volume des SFX
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    // Toggle et label pour le plein écran
    [SerializeField] Toggle fullScreenToggle;
    [SerializeField] Toggle devModToggle;

    // TMP_Dropdown pour la qualité visuelle
    [SerializeField] TMP_Dropdown qualityDrop;

    private bool isActive = false;

    void Start()
    {
        // Initialiser les volumes
        SetVolume(PlayerPrefs.GetFloat("Music", 0.5f));
        SetSfxVolume(PlayerPrefs.GetFloat("Sfx", 0.5f));

        musicSlider.value = PlayerPrefs.GetFloat("Music", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("Sfx", 0.5f);

        // Initialiser la liste des niveaux de qualité
        qualityDrop.ClearOptions();
        List<string> options = new List<string>();

        int currentQualityIndex = QualitySettings.GetQualityLevel();
        string[] qualityLevels = QualitySettings.names;

        for (int i = 0; i < qualityLevels.Length; i++)
        {
            options.Add(qualityLevels[i]);
        }

        qualityDrop.AddOptions(options);
        qualityDrop.value = currentQualityIndex;
        qualityDrop.RefreshShownValue();

        // Initialiser le mode plein écran
        fullScreenToggle.isOn = Screen.fullScreen;

        // Ajouter des listeners
        musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider.value); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSfxVolume(sfxSlider.value); });
        fullScreenToggle.onValueChanged.AddListener(SetPleinEcran);
        qualityDrop.onValueChanged.AddListener(SetQuality);
    }

    // Ajuster le volume principal
    public void SetVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * 20 - 20;
        audioMixer.SetFloat("Music", dB);
        PlayerPrefs.SetFloat("Music", volume);
    }

    // Ajuster le volume des SFX
    public void SetSfxVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("Sfx", dB);
        PlayerPrefs.SetFloat("Sfx", volume);
    }

    // Activer/désactiver le plein écran
    public void SetPleinEcran(bool isPleinEcran)
    {
        Screen.fullScreen = isPleinEcran;
    }

    // Méthode pour afficher ou masquer les statistiques de performance
    public void ShowPerformanceStats()
    {
        isActive = !isActive; // Inverse la visibilité
        performances.SetActive(isActive);
    }

    // Changer la qualité visuelle
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}