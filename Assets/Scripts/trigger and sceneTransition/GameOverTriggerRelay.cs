using UnityEngine;

public class GameOverTriggerRelay : MonoBehaviour
{
    private GameOverTrigger parentTrigger;

    private void Awake()
    {
        parentTrigger = GetComponentInParent<GameOverTrigger>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<playerController>() != null)
        {
            if (parentTrigger != null)
            {
                parentTrigger.triggerGameOver();
            }
        }
    }
}