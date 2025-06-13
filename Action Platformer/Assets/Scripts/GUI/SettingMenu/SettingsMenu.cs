using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _resolutionDropDown;
    [SerializeField]
    private TMP_Dropdown _qualityDropDown;
    [SerializeField]
    private UnityEngine.UI.Toggle _fullscreenToggle;

    private Resolution[] _resolutions;
    private List<string> _options;

    private const string QualityKey = "quality";
    private const string FullScreenKey = "fullscreen";
    private const string ResolutionKey = "resolution";

    private int _currentQuality;
    private bool _currentFullScreen;
    private int _currentResolutionIndex;

    private void Start()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropDown.ClearOptions();
        _options = new List<string>();

        int defaultResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            _options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                defaultResolutionIndex = i;
            }
        }

        _resolutionDropDown.AddOptions(_options);

        _currentQuality = PlayerPrefs.GetInt(QualityKey, QualitySettings.GetQualityLevel());
        _currentFullScreen = PlayerPrefs.GetInt(FullScreenKey, Screen.fullScreen ? 1 : 0) == 1;
        _currentResolutionIndex = PlayerPrefs.GetInt(ResolutionKey, defaultResolutionIndex);

        ApplySettingsToSystem();

        _qualityDropDown.value = _currentQuality;
        _fullscreenToggle.isOn = _currentFullScreen;
        _resolutionDropDown.value = _currentResolutionIndex;

        _qualityDropDown.onValueChanged.AddListener(SetQuality);
        _fullscreenToggle.onValueChanged.AddListener(SetFullScreen);
        _resolutionDropDown.onValueChanged.AddListener(SetResolution);
    }

    public void SetQuality(int qualityIndex)
    {
        _currentQuality = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _currentFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex < 0 || resolutionIndex >= _resolutions.Length) return;

        _currentResolutionIndex = resolutionIndex;

        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, _currentFullScreen);
    }

    private void ApplySettingsToSystem()
    {
        QualitySettings.SetQualityLevel(_currentQuality);
        Screen.SetResolution(_resolutions[_currentResolutionIndex].width, _resolutions[_currentResolutionIndex].height, _currentFullScreen);
    }

    public void SaveButtonPressed()
    {
        PlayerPrefs.SetInt(QualityKey, _currentQuality);
        PlayerPrefs.SetInt(FullScreenKey, _currentFullScreen ? 1 : 0);
        PlayerPrefs.SetInt(ResolutionKey, _currentResolutionIndex);

        PlayerPrefs.Save();

        Debug.Log("Game Saved");
    }

    public void ExitButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}