using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject mainCanvas;

    private bool isPaused = false;

    private void Awake()
    {
        findPauseUIReferences();
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
        findPauseUIReferences();
        UpdatePauseUI(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        findPauseUIReferences();
        UpdatePauseUI(scene.name);
    }

    private void UpdatePauseUI(string sceneName)
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseButton == null || pausePanel == null)
            return;

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
        findPauseUIReferences();

        if (pausePanel == null)
            return;

        isPaused = true;
        pausePanel.SetActive(true);
        pausePanel.transform.SetAsLastSibling();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        findPauseUIReferences();

        if (pausePanel == null)
            return;

        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        findPauseUIReferences();

        isPaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (mainCanvas != null)
        {
            mainCanvas.SetActive(false);
        }

        SceneManager.LoadScene("MainMenu");
    }

    private void findPauseUIReferences()
    {
        if (mainCanvas == null)
        {
            GameObject canvasObject = GameObject.Find("UICanvas");

            if (canvasObject != null)
            {
                mainCanvas = canvasObject;
            }
        }

        if (pauseButton == null)
        {
            GameObject pauseButtonObject = GameObject.Find("pauseButton");

            if (pauseButtonObject != null)
            {
                pauseButton = pauseButtonObject;
            }
        }

        if (pausePanel == null && mainCanvas != null)
        {
            Transform pausePanelTransform = mainCanvas.transform.Find("pausePanel");

            if (pausePanelTransform != null)
            {
                pausePanel = pausePanelTransform.gameObject;
            }
        }
    }
}
