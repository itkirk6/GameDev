using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Popup Panel")]
    public GameObject HowToPlayPanel;
    public GameObject UICanvas;

    private void Start()
    {
        if (HowToPlayPanel != null)
        {
            HowToPlayPanel.SetActive(false);
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("prison");
        UICanvas.SetActive(true);
    }

    public void OpenHowToPlay()
    {
        HowToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        HowToPlayPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}