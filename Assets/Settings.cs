using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button exitButton;
    private bool isOpened;

    private void Awake()
    {
        closeButton.onClick.AddListener(Close);
        exitButton.onClick.AddListener(Exit);
    }
    public void Open()
    {
        if (isOpened)
        {
            gameObject.SetActive(false);
            isOpened = false;

        } else
        {
            gameObject.SetActive(true);
            isOpened = true;
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void Exit()
    {
        GameManager.LoadLevel(0);
    }
}