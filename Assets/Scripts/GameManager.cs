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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentScene != 0)
        {
            LoadLevel(0);
        }
    }

    public static void LoadLevel(int levelIndex)
    {
        currentScene = levelIndex;
        //BackgroundMusic.Instance.ChangeClip(levelIndex);
        SceneManager.LoadScene(levelIndex);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
