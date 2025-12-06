using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject creditsImg;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ShowCredits()
    {
        creditsImg.SetActive(true);
    }

    public void HideCredits()
    {
        creditsImg.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
UnityEngine.Application.OpenURL("about:blank");

#else
Application.Quit(); 

#endif
    }
}
