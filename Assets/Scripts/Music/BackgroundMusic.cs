using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;

    [field: SerializeField] public float Volume { get; private set; } = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource.volume = Volume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }

    public void ChangeClip(int index)
    {
        if (index >= 0 && index < audioClips.Count)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play(); // Запуск музыки после смены клипа
        }
    }

    public void OnVolumeChanged(float newVolume)
    {
        Volume = newVolume;
        audioSource.volume = Volume;
    }
}
