using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager Instance;
    private static AudioSource _audioSource;
    private static SoundEffectLibrary _soundEffectLibrary;

    [SerializeField]
    private Slider _slider;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            _soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _slider.onValueChanged.AddListener(delegate { OnValueChanged();  });
    }

    public static void PlaySound(string soundName)
    {
        AudioClip audioClip = _soundEffectLibrary.GetRandomClip(soundName);

        if(audioClip != null)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }

    public static void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    public void OnValueChanged()
    {
        SetVolume(_slider.value);
    }
}

