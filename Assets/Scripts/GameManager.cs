using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static int currentScene;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void LoadLevel(int levelIndex = -1)
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (levelIndex == -1)
        {
            SceneManager.LoadScene(++currentScene);
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        if (levelIndex == 0) Cursor.lockState = CursorLockMode.None;
        currentScene = levelIndex;
        SceneManager.LoadScene(levelIndex);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
