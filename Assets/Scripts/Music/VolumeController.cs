using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = BackgroundMusic.Instance.Volume;
            volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
        }
    }

    private void OnVolumeSliderChanged(float value)
    {
        if (BackgroundMusic.Instance != null)
        {
            BackgroundMusic.Instance.OnVolumeChanged(value);
        }
    }
}
