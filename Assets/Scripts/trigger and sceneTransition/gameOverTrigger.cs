using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip gameOverSound;

    private AudioSource audioSource;
    public GameObject gameOverPanel;
    private GameObject uiCanvas;

    private bool isGameOver = false;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        findGameOverPanel();
        findUICanvas();
    }

    public void triggerGameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            findGameOverPanel();

            if (gameOverPanel != null)
            {
                findUICanvas();

                if (uiCanvas != null)
                {
                    uiCanvas.SetActive(false);
                }

                gameOverPanel.SetActive(true);
            }

            audioSource.PlayOneShot(gameOverSound);
        }
    }


    private void findGameOverPanel()
    {
        if (gameOverPanel != null)
            return;

        GameObject panelObject = GameObject.FindGameObjectWithTag("WinPanel");

        if (panelObject != null)
        {
            gameOverPanel = panelObject;
            gameOverPanel.SetActive(false);
        }
    }

    private void findUICanvas()
    {
        if (uiCanvas != null)
            return;

        uiCanvas = GameObject.Find("UICanvas");
    }

}
