using System.Collections.Generic;
using UnityEngine;

public class SetAudio : MonoBehaviour
{
    [SerializeField] private List<AudioSource> mAudioSources;

    private void Start()
    {
        foreach (AudioSource source in mAudioSources)
        {
            source.volume = BackgroundMusic.Instance.Volume;
        }
        BackgroundMusic.OnVolumeChangedEvent.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged()
    {
        foreach (AudioSource source in mAudioSources)
        {
            source.volume = BackgroundMusic.Instance.Volume;
        }
    }
}
