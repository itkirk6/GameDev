using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Tooltip("The name of the key that is required to open this specific door. e.g. cafeteria")]
    public string requiredKeyName;
    public Sprite closedSprite;
    public Sprite openSprite;

    public SpriteRenderer spriteRenderer;
    public Collider2D blockingCollider;
    private bool isOpen = false;
    private bool isLocked = true;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateDoorVisual();
    }
    private void UpdateDoorVisual()
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

    public void TryOpen()
    {
        PlayerInventory inv = FindFirstObjectByType<PlayerInventory>();

        if(isLocked)
        {
            if(inv != null && inv.HasKey(requiredKeyName) || requiredKeyName.ToLower() == "none")
            {
                isLocked = false;
                Debug.Log("unlocked door with " + requiredKeyName);
            }
            else
            {
                Debug.Log("door is locked, requires " + requiredKeyName);
                return;
            }
        }

        if(isOpen)
            CloseDoor();
        else
            OpenDoor();
    }

    private void OpenDoor()
    {
        isOpen = true;
        UpdateDoorVisual();
        Debug.Log("Door opened");
    }

    private void CloseDoor()
    {
        isOpen = false;
        UpdateDoorVisual();
        Debug.Log("Door closed");
    }
}