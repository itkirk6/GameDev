using UnityEditor.Toolbars;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRadius = 2f;
    public LayerMask interactableLayer;
    public GameObject currentItemPrefab;

    private IUsable currentUsableItem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            TryInteract();

        if(Input.GetMouseButtonDown(0))
            if( currentUsableItem != null)
                currentUsableItem.UseItem();
    }

    private void TryInteract()
    {
        Collider2D[] interactableColliders = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactableLayer);

        foreach(Collider2D collider in interactableColliders)
        {
            IInteractable interactable = collider.GetComponentInParent<IInteractable>();
            if(interactable != null)
            {
                interactable.Interact(this);
                break;
            }
        }
    }

    public void EquipItem(GameObject newItemPrefab)
    {
        if (currentItemPrefab != null)
            Instantiate(currentItemPrefab, transform.position, Quaternion.identity);
        
        currentItemPrefab = newItemPrefab;
    }

    public void UseItem()
    {
        ItemInfo info = currentItemPrefab.GetComponent<ItemInfo>();
        if(info.itemName == "gun")
        {
            //TODO: Instantiate bullet 
            //TODO: Create bullet prefab and scripts
        }

        
    }
}
