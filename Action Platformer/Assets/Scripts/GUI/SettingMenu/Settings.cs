using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _resolutionDropDown;
    [SerializeField]
    private TMP_Dropdown _qualityDropDown;

    private Resolution[] _resolutions;

    private void Start()
    {
        _resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        _resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height + " " + _resolutions[i].refreshRateRatio + "Hz";
            options.Add(option);
            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropDown.AddOptions(options);
        _resolutionDropDown.RefreshShownValue();
        LoadSettings();
    }

    public void SetFullScreen(bool isFullScreen) => Screen.fullScreen = isFullScreen;

    public void SetResplution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);

    public void ExitSettings() => SceneManager.LoadScene("MainMenu");

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", _qualityDropDown.value);
        PlayerPrefs.SetInt("ResolutionPreference", _resolutionDropDown.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));
    }

    public void LoadSettings()
    {
        // Загрузка качества
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
        {
            int quality = PlayerPrefs.GetInt("QualitySettingPreference");
            _qualityDropDown.value = quality;
            QualitySettings.SetQualityLevel(quality);
        }
        else
        {
            _qualityDropDown.value = 0; // Низкое качество (первый элемент в DropDown)
            QualitySettings.SetQualityLevel(0);
        }

        // Загрузка разрешения
        if (PlayerPrefs.HasKey("ResolutionPreference"))
        {
            int resolutionIndex = PlayerPrefs.GetInt("ResolutionPreference");
            _resolutionDropDown.value = resolutionIndex;
            Resolution resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        else
        {
            // Устанавливаем последний элемент (1920x1080) как разрешение по умолчанию
            int defaultResolutionIndex = _resolutions.Length - 1;
            _resolutionDropDown.value = defaultResolutionIndex;
            Resolution resolution = _resolutions[defaultResolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        // Загрузка полноэкранного режима
        if (PlayerPrefs.HasKey("FullscreenPreference"))
        {
            bool fullscreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
            Screen.fullScreen = fullscreen;
        }
        else
        {
            Screen.fullScreen = true; // Полноэкранный режим по умолчанию
        }

        // Обновляем значения в UI
        _qualityDropDown.RefreshShownValue();
        _resolutionDropDown.RefreshShownValue();
    }
}