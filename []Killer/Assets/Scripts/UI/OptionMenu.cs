using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown graphicDropdown;
    public Toggle fulscreenToggle;
    public Slider volumeSlider;
    Resolution[] resolutions;

    private void Start () {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions ();
        List<string> options = new List<string> ();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (!options.Contains (option))
                options.Add (option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions (options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue ();

        int qualitySettings = QualitySettings.GetQualityLevel ();
        graphicDropdown.value = qualitySettings;
        graphicDropdown.RefreshShownValue ();

        bool isFullscreen = Screen.fullScreen;
        fulscreenToggle.isOn = isFullscreen;

        float volume = 0f;
        audioMixer.GetFloat ("volume", out volume);
        volumeSlider.value = volume;

        gameObject.SetActive(false);
    }

    public void SetVolume (float volume) {
        audioMixer.SetFloat ("volume", volume);
    }

    public void SetQuality (int qualityIndex) {
        QualitySettings.SetQualityLevel (qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution (int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
    }
}
