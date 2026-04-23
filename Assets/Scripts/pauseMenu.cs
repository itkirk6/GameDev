using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public static PauseUI instance;

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pausePanel;

    private bool isPaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        UpdatePauseUI(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdatePauseUI(scene.name);
    }

    private void UpdatePauseUI(string sceneName)
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (sceneName == "MainMenu")
        {
            pauseButton.SetActive(false);
            pausePanel.SetActive(false);
        }
        else
        {
            pauseButton.SetActive(true);
            pausePanel.SetActive(false);
        }
    }

    public void OpenPausePanel()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}