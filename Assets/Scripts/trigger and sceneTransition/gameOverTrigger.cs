using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private AudioClip gameOverSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    public void triggerGameOver()
    {
        gameOverCanvas.SetActive(true);
        audioSource.PlayOneShot(gameOverSound);
    }
}