using UnityEngine;
using UnityEngine.Audio;

public class LoadSettings : MonoBehaviour
{
    private const string _qualityKey = "quality";
    private const string _fullScreenKey = "fullscreen";
    private const string _resolutionKey = "resolution";

    private Resolution[] _resolutions;

    private void Awake()
    {
        ApplySettings();
    }

    private void ApplySettings()
    {
        int qualityIndex = PlayerPrefs.GetInt(_qualityKey, QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(qualityIndex);

        bool isFullScreen = PlayerPrefs.GetInt(_fullScreenKey, Screen.fullScreen ? 1 : 0) == 1;

        _resolutions = Screen.resolutions;
        int resolutionIndex = PlayerPrefs.GetInt(_resolutionKey, -1);

        if (resolutionIndex >= 0 && resolutionIndex < _resolutions.Length)
        {
            Resolution res = _resolutions[resolutionIndex];
            Screen.SetResolution(res.width, res.height, isFullScreen);
        }
        else
        {
            Screen.fullScreen = isFullScreen;
        }
    }
}

