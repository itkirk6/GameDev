using UnityEngine;

public class doorController : MonoBehaviour, IInteractable
{
    public bool isLocked;
    public bool isOpen;
    public keyTypes requiredKey = keyTypes.none;

    public Sprite closedSprite;
    public Sprite openSprite;

    public SpriteRenderer spriteRenderer;
    public Collider2D blockingCollider;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        updateDoorVisual();
    }

    public void Interact(PlayerInteraction playerInteraction)
    {
        playerInventory playerInv = playerInteraction.GetComponent<playerInventory>();

        if (isLocked)
        {
            if (playerInv != null && playerInv.hasKey(requiredKey))
            {
                isLocked = false;
                Debug.Log("unlocked door with " + requiredKey);
            }
            else
            {
                Debug.Log("door is locked, requires " + requiredKey);
                return;
            }
        }

        if (isOpen)
            closeDoor();
        else
            openDoor();
    }

    private void openDoor()
    {
        isOpen = true;
        updateDoorVisual();
    }

    private void closeDoor()
    {
        isOpen = false;
        updateDoorVisual();
    }

    private void updateDoorVisual()
    {
        if (spriteRenderer != null)
        {
            if (isOpen)
                spriteRenderer.sprite = openSprite;
            else
                spriteRenderer.sprite = closedSprite;
        }

        if (blockingCollider != null)
            blockingCollider.enabled = !isOpen;
    }
}