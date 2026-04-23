using System.Collections;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum PickupType {Equippable, Key, Consumable, QuestItem}
    public string itemName;
    public PickupType itemType;
    public int itemValue;
    public float pickupRange = 3f;
    public void Collect()
    {
        PlayerInventory inv = FindFirstObjectByType<PlayerInventory>();
        if(inv != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, inv.transform.position);
            if(distanceToPlayer <= pickupRange)
                inv.ProcessPickup(this);
            else
                Debug.Log($"{itemName} is too far away");
        }
    }
}
