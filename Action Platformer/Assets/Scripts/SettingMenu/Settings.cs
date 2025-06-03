using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] 
    private TMP_Dropdown _resolutionDropdown;
    [SerializeField] 
    private TMP_Dropdown _qualityDropdown;

    private Resolution[] _availableResolutions;
    private const int _defaultQualityLevel = 3;

    private void Start()
    {
        InitializeResolutionDropdown();
        InitializeQualityDropdown();
        LoadSettings();
    }

    private void InitializeResolutionDropdown()
    {
        _resolutionDropdown.ClearOptions();
        _availableResolutions = Screen.resolutions;

        var options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < _availableResolutions.Length; i++)
        {
            var resolution = _availableResolutions[i];
            string option = $"{resolution.width}x{resolution.height} {resolution.refreshRateRatio.value:0}Hz";
            options.Add(option);

            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    private void InitializeQualityDropdown()
    {
        _qualityDropdown.ClearOptions();
        _qualityDropdown.AddOptions(new List<string>(QualitySettings.names));
        _qualityDropdown.value = QualitySettings.GetQualityLevel();
        _qualityDropdown.RefreshShownValue();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex < 0 || resolutionIndex >= _availableResolutions.Length) return;

        var resolution = _availableResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", _qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", _resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        _qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference", _defaultQualityLevel);
        QualitySettings.SetQualityLevel(_qualityDropdown.value);

        int defaultResolution = FindDefaultResolutionIndex();
        _resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference", defaultResolution);
        SetResolution(_resolutionDropdown.value);

        Screen.fullScreen = PlayerPrefs.GetInt("FullscreenPreference", 1) == 1;
    }

    private int FindDefaultResolutionIndex()
    {
        for (int i = 0; i < _availableResolutions.Length; i++)
        {
            if (_availableResolutions[i].width == Screen.currentResolution.width &&
                _availableResolutions[i].height == Screen.currentResolution.height)
            {
                return i;
            }
        }
        return 0;
    }
}