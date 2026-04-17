using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("doors State")]
    public bool isOpen = false;
    public bool isLocked = false;

    [Header("key required")]
    public string requiredKeyID = "";

    [Header("door images")]
    public SpriteRenderer spriteRend;
    public Sprite closedImg;
    public Sprite openImg;
    [Header("collider")]
    public Collider2D doorCollider;

    public void interact(PlayerInventory playerInventory)   //change this for the player inventory for keys to work
    {
        if (isLocked)
        {
            if (playerInventory == null)
            {
                Debug.Log("no player inventory found.");
                return;
            }
  
            if (!playerInventory.HasKey(requiredKeyID))
            {
                Debug.Log("door is locked. this key unlocks: " + requiredKeyID);
                return;
            }
        } 
  
        if (isOpen)
        {
            closeDoor();
        }
        else 
        {
            openDoor();
        }  
    }

    public void openDoor()
    {
        isOpen = true;

        if (spriteRend != null && openImg != null)
            spriteRend.sprite = openImg;

        if (doorCollider != null)
            doorCollider.enabled = false;
    }

    public void closeDoor()
    {
        isOpen = false;

        if (spriteRend != null && closedImg != null)
            spriteRend.sprite = closedImg;

        if (doorCollider != null)
            doorCollider.enabled = true;
    }
}