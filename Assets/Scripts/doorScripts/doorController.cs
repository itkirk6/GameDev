using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Tooltip("The name of the key that is required to open this specific door. e.g. cafeteria")]
    public string requiredKeyName;
    public Sprite closedSprite;
    public Sprite openSprite;

    [SerializeField] private AudioClip openDoorClip;
    [SerializeField] private AudioClip closeDoorClip;

    public SpriteRenderer spriteRenderer;
    public Collider2D blockingCollider;
    private bool isOpen = false;
    private bool isLocked = true;
    public bool guardsCanAutoOpen = false;
    public string guardTag = "Guard";
    private int guardsInTrigger = 0;
    private bool openedByGuard = false;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

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

        if (isOpen)
        {
            closeDoor();
        }
        else
        {
            openDoor();
        }
    }

    private void openDoor()
    {
        if (openDoorClip != null)
        {
            audioSource.PlayOneShot(openDoorClip);
        }

        isOpen = true;
        updateDoorVisual();
        //Debug.Log("Door opened");
    }

    private void closeDoor()
    {
        if (closeDoorClip != null)
        {
            audioSource.PlayOneShot(closeDoorClip);
        }

        isOpen = false;
        updateDoorVisual();
        //Debug.Log("Door closed");
    }
    public void guardEnteredDoorTrigger()
    {
        if (!guardsCanAutoOpen)
            return;

        guardsInTrigger++;

        if (!isOpen)
        {
            isLocked = false;
            openDoor();
            openedByGuard = true;
        }
    }

    public void guardExitedDoorTrigger()
    {
        if (!guardsCanAutoOpen)
            return;

        guardsInTrigger = Mathf.Max(0, guardsInTrigger - 1);

        if (guardsInTrigger == 0 && openedByGuard)
        {
            closeDoor();
            openedByGuard = false;
        }
    }

}