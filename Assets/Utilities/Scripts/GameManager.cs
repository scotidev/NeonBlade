using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerRef;

    [SerializeField] private GameObject pauseObj;
    private bool isPaused = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void setPlayerRef(GameObject newPlayer)
    {
        playerRef = newPlayer;
    }

    public GameObject getPlayerRef()
    {
        return playerRef;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseObj.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseObj.SetActive(false);
        isPaused = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        UnityEngine.Application.OpenURL("about:blank");
#else
        Application.Quit();
#endif
    }
}

