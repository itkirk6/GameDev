 using UnityEditor.Toolbars;
 using UnityEngine;

 public class PlayerInteraction : MonoBehaviour
 {
     public float interactRadius = 2f;
     public LayerMask interactableLayer;
     public LayerMask pickupLayer;
     public GameObject currentItemPrefab;

     void Start()
     {
        
     }

     // Update is called once per frame
     void Update()
     {
         if (Input.GetKeyDown(KeyCode.E))
         {
             TryInteract();
             Debug.Log("poressed e");
         }
         if (Input.GetKeyDown(KeyCode.F))
             TryPickup();

     }

     private void TryInteract()
     {
         Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactableLayer);

         IInteractable closestInteractable = null;
         float closestDistance = Mathf.Infinity;

         foreach (Collider2D collider in colliders)
         {
             IInteractable interactable = collider.GetComponentInParent<IInteractable>();

             if (interactable != null)
             {
                 float distance = Vector2.Distance(transform.position, collider.transform.position);

                 if (distance < closestDistance)
                 {
                     closestDistance = distance;
                     closestInteractable = interactable;
                 }
             }
         }

         if (closestInteractable != null)
         {
             Debug.Log("found interactable");
             closestInteractable.Interact(this);
         }
         else
         {
             Debug.Log("no interactable found");
         }
     }
     private void TryPickup()
     {
         Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius, pickupLayer);

         IPickup closestPickup = null;
         float closestDistance = Mathf.Infinity;

         foreach (Collider2D collider in colliders)
         {
             IPickup pickup = collider.GetComponentInParent<IPickup>();

            if (pickup != null)
             {
                 float distance = Vector2.Distance(transform.position, collider.transform.position);

                if (distance < closestDistance)
                 {
                     Debug.Log("found pickup");
                     closestDistance = distance;
                     closestPickup = pickup;
                 }
             }
         }

         if (closestPickup != null)
             closestPickup.pickup(this);
     }

     public void EquipItem(GameObject newItemPrefab)
     {
         if (currentItemPrefab != null)
             Instantiate(currentItemPrefab, transform.position, Quaternion.identity);
        
         currentItemPrefab = newItemPrefab;
     }
/*
     public void UseItem()
     {
         ItemInfo info = currentItemPrefab.GetComponent<ItemInfo>();
         if(info.itemName == "gun")
         {
             //TODO: Instantiate bullet 
            //TODO: Create bullet prefab and scripts
       }

        
    }
*/
 }
