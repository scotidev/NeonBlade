using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject playerRef;

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPlayerRef(GameObject newPlayer)
    {
        playerRef = newPlayer;
    }

    public GameObject getPlayerRef()
    {
        return playerRef;
    }
}
