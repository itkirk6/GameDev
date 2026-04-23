using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip gameOverSound;

    private AudioSource audioSource;
    public GameObject gameOverPanel;

    private bool isGameOver = false;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        findGameOverPanel();
    }

    public void triggerGameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            findGameOverPanel();

            if (gameOverPanel != null)
            {
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

}
