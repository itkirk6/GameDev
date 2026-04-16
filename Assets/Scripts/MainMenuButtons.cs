using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Popup Panel")]
    public GameObject HowToPlayPanel;

    private void Start()
    {
        if (HowToPlayPanel != null)
        {
            HowToPlayPanel.SetActive(false);
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Prison");
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