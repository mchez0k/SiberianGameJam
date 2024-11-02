using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;

    //[SerializeField] private float volume;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }

    public void ChangeClip(int index)
    {
        audioSource.clip = audioClips[index];
    }

    public void OnVolumeChanged(float volume)
    {
        //this.volume = volume;
        audioSource.volume = volume;
    }
}
