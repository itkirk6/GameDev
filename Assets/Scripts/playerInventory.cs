using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // private HashSet<keyTypes> keys = new HashSet<keyTypes>();

    // public void addKey(keyTypes key)
    // {
    //     if (key != keyTypes.none)
    //     {
    //         keys.Add(key);
    //         Debug.Log("picked up key: " + key);
    //     }
    // }

    // public bool hasKey(keyTypes key)
    // {
    //     if (key == keyTypes.none)
    //         return true;

    //     return keys.Contains(key);
    // }

    public string currentEquippedItem = "None";
    public string previouslyEquippedItem;
    public List<string> keys = new List<string>();
    public int playerHealth = 100;

    public void ProcessPickup( ItemPickup pickupInfo)
    {
        switch(pickupInfo.itemType)
        {
            case ItemPickup.PickupType.Equippable:
                previouslyEquippedItem = currentEquippedItem;
                currentEquippedItem = pickupInfo.itemName;
                Debug.Log("Equipped: {currentEquippedItem}");
                break;
            
            case ItemPickup.PickupType.Consumable:
                playerHealth += pickupInfo.itemValue;
                Debug.Log("Restored {pickupInfo.itemValue} health.");
                break;

            case ItemPickup.PickupType.Key:
                if(!keys.Contains(pickupInfo.itemName))
                {
                    keys.Add(pickupInfo.itemName);
                    Debug.Log("Added {pickupInfo.itemName} key to inventory.");
                }
                break;
        }
    }

    public bool HasKey(string requiredKey)
    {
        return keys.Contains(requiredKey);
    }

}