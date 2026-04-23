using UnityEngine;
using UnityEngine.SceneManagement;

public class deathMenu : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";

    public void goToMainMenu()
    {
        Time.timeScale = 1f;

        playerController player = FindFirstObjectByType<playerController>();

        if (player != null)
        {
            Destroy(player.gameObject);
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
