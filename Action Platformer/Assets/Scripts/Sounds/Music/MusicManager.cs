using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private Slider _slider;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(_audioClip != null)
        {
            PlayBackgroundMusic(false, _audioClip);
        }

        _slider.onValueChanged.AddListener(delegate { SetVolume(_slider.value); });
    }

    public static void SetVolume(float volume)
    {
        Instance._audioSource.volume = volume;
    }

    public static void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        if(audioClip != null)
        {
            Instance._audioSource.clip = audioClip;
        }

        if(Instance._audioSource.clip != null)
        {
            if (resetSong)
            {
                Instance._audioSource.Stop();
            }

            Instance._audioSource.Play();
        }
    }

    public static void PauseBackgroundMusic()
    {
        Instance._audioSource.Pause();
    }
}

