using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider volumeSlider;

    private bool isOpened;

    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener(BackgroundMusic.Instance.OnVolumeChanged);
        closeButton.onClick.AddListener(Close);
        exitButton.onClick.AddListener(Exit);
    }

    public void Open()
    {
        if (isOpened)
        {
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpened = false;

        } else
        {
            Cursor.lockState = CursorLockMode.None;
            gameObject.SetActive(true);
            isOpened = true;
        }
    }

    public void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }

    private void Exit()
    {
        Cursor.lockState = CursorLockMode.None;
        GameManager.LoadLevel(0);
    }
}
