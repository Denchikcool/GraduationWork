using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;
    [SerializeField]
    private TMP_Dropdown _resolutionDropDown;
    [SerializeField]
    private TMP_Dropdown _qualityDropDown;
    [SerializeField]
    private UnityEngine.UI.Toggle _fullscreenToggle;
    [SerializeField]
    private UnityEngine.UI.Slider _volumeSlider;

    private Resolution[] _resolutions;
    private List<string> _options;

    // Ключи для PlayerPrefs
    private const string VolumeKey = "volume";
    private const string QualityKey = "quality";
    private const string FullScreenKey = "fullscreen";
    private const string ResolutionKey = "resolution";

    // Текущие выбранные настройки (не обязательно сохранённые)
    private float _currentVolume;
    private int _currentQuality;
    private bool _currentFullScreen;
    private int _currentResolutionIndex;

    private void Start()
    {
        // Получаем доступные разрешения
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

        // Загружаем сохранённые настройки из PlayerPrefs, если они есть, иначе берём текущие системные
        _currentVolume = PlayerPrefs.GetFloat(VolumeKey, 0f);
        _currentQuality = PlayerPrefs.GetInt(QualityKey, QualitySettings.GetQualityLevel());
        _currentFullScreen = PlayerPrefs.GetInt(FullScreenKey, Screen.fullScreen ? 1 : 0) == 1;
        _currentResolutionIndex = PlayerPrefs.GetInt(ResolutionKey, defaultResolutionIndex);

        // Применяем настройки к системе
        ApplySettingsToSystem();

        // Обновляем UI чтобы отобразить текущие значения
        _volumeSlider.value = _currentVolume;
        _qualityDropDown.value = _currentQuality;
        _fullscreenToggle.isOn = _currentFullScreen;
        _resolutionDropDown.value = _currentResolutionIndex;

        // Подписываемся на изменения UI элементов, чтобы менять текущие значения при взаимодействии
        _volumeSlider.onValueChanged.AddListener(SetVolume);
        _qualityDropDown.onValueChanged.AddListener(SetQuality);
        _fullscreenToggle.onValueChanged.AddListener(SetFullScreen);
        _resolutionDropDown.onValueChanged.AddListener(SetResolution);
    }

    // Методы, вызываемые при изменении UI — обновляют текущие настройки и применяют их, но не сохраняют
    public void SetVolume(float volume)
    {
        _currentVolume = volume;
        _audioMixer.SetFloat("volume", volume);
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

    // Применяем текущие настройки к системе (например, при старте)
    private void ApplySettingsToSystem()
    {
        _audioMixer.SetFloat("volume", _currentVolume);
        QualitySettings.SetQualityLevel(_currentQuality);
        Screen.SetResolution(_resolutions[_currentResolutionIndex].width, _resolutions[_currentResolutionIndex].height, _currentFullScreen);
    }

    // Кнопка "Сохранить" — сохраняет текущие настройки в PlayerPrefs
    public void SaveButtonPressed()
    {
        PlayerPrefs.SetFloat(VolumeKey, _currentVolume);
        PlayerPrefs.SetInt(QualityKey, _currentQuality);
        PlayerPrefs.SetInt(FullScreenKey, _currentFullScreen ? 1 : 0);
        PlayerPrefs.SetInt(ResolutionKey, _currentResolutionIndex);

        PlayerPrefs.Save();

        Debug.Log("Настройки сохранены");
    }

    // Кнопка "Выход" — возвращает в главное меню без сохранения
    public void ExitButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
