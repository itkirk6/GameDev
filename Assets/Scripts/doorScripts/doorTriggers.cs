using UnityEngine;

public class doorTriggers : MonoBehaviour
{
    public DoorController doorController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Guard"))
            return;

        if (doorController != null)
        {
            doorController.guardEnteredDoorTrigger();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Guard"))
            return;

        if (doorController != null)
        {
            doorController.guardExitedDoorTrigger();
        }
    }
}

