using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";
    public GameObject winPanel;

    public void goToMainMenu()
    {
        Time.timeScale = 1f;

        playerController player = FindFirstObjectByType<playerController>();

        if (player != null)
        {
            Destroy(player.gameObject);
        }

        winPanel.SetActive(false);
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
