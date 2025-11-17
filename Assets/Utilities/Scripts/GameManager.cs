using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerRef;

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

    public void setPlayerRef(GameObject newPlayer)
    {
        playerRef = newPlayer;
    }

    public GameObject getPlayerRef()
    {
        return playerRef;
    }

    public void StartGame()
    {
        Debug.Log("Game Started");
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Debug.Log("Game Resumed");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
