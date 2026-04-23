using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip gameOverSound;

    private AudioSource audioSource;
    private PlayerInventory playerInventory;
    public GameObject gameOverPanel;

    private bool isGameOver = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        playerInventory = FindFirstObjectByType<PlayerInventory>();
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
            playerInventory.handleGameOverPanel();
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