using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // public string currentEquippedItem = "None";
    // public string previouslyEquippedItem;
    public List<string> keys = new List<string>();
    public List<string> questItems = new List<string>();
    public int playerHealth = 100;
    public Image equippedItemUIBox;
    public GameObject currentEquippedItem;

    private void Awake()
    {

    }
    void Start()
    {
        if(equippedItemUIBox != null)
            equippedItemUIBox.enabled = false;
    }

    void Update()
    {
        if(playerHealth <= 0)
        {
            Debug.Log("Player is dead");
            Destroy(gameObject);
        }
        if(Input.GetMouseButtonDown(0) && currentEquippedItem != null)
        {
            IUsableItem usableItem = currentEquippedItem.GetComponent<IUsableItem>();
            if(usableItem != null)
            {
                usableItem.UseItem();
            }
        }
    }
    public void ProcessPickup( ItemPickup pickupInfo)
    {
        Debug.Log("ProcessPickup() was called");
        switch(pickupInfo.itemType)
        {
            case ItemPickup.PickupType.Equippable:

                if(currentEquippedItem != null)
                {
                    currentEquippedItem.transform.SetParent(null);
                    currentEquippedItem.transform.localScale = currentEquippedItem.GetComponent<InteractableObject>().originalScale;
                    currentEquippedItem.transform.position = transform.position;
                    currentEquippedItem.GetComponent<SpriteRenderer>().enabled = true;
                    currentEquippedItem.GetComponent<Collider2D>().enabled = true;
                    currentEquippedItem.GetComponent<InteractableObject>().enabled = true;
                    currentEquippedItem.GetComponent<ItemPickup>().enabled = true;

                }

                currentEquippedItem = pickupInfo.gameObject;

                currentEquippedItem.transform.SetParent(transform);
                currentEquippedItem.transform.position = Vector3.zero;

                currentEquippedItem.GetComponent<SpriteRenderer>().enabled = false;
                currentEquippedItem.GetComponent<Collider2D>().enabled = false;

                currentEquippedItem.GetComponent<InteractableObject>().enabled = false;;
                currentEquippedItem.GetComponent<ItemPickup>().enabled = false;

                if(equippedItemUIBox != null)
                {
                    equippedItemUIBox.sprite = pickupInfo.GetComponent<SpriteRenderer>().sprite;
                    equippedItemUIBox.enabled = true;
                    equippedItemUIBox.color = Color.white;
                }

                Debug.Log($"Equipped: {currentEquippedItem}");
                break;
            
            case ItemPickup.PickupType.Consumable:
                playerHealth += pickupInfo.itemValue;
                Debug.Log($"Restored {pickupInfo.itemValue} health.");
                Destroy(pickupInfo.gameObject);
                break;

            case ItemPickup.PickupType.Key:
                if(!keys.Contains(pickupInfo.itemName))
                {
                    keys.Add(pickupInfo.itemName);
                    Debug.Log($"Added {pickupInfo.itemName} key to inventory.");
                }
                Destroy(pickupInfo.gameObject);
                break;
            
            case ItemPickup.PickupType.QuestItem:
                //Debug.Log("Enterd here");
                if(!questItems.Contains(pickupInfo.itemName))
                {
                    questItems.Add(pickupInfo.itemName);
                    Debug.Log($"Added {pickupInfo.itemName} to inventory.");
                }
                Destroy(pickupInfo.gameObject);
                break;

        }
    }

    public bool HasKey(string requiredKey)
    {
        return keys.Contains(requiredKey);
    }

}