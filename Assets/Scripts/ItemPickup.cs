using System.Collections;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum PickupType {Equippable, Key, Consumable}
    public GameObject itemPrefab;
    public string itemName;
    public PickupType itemType;
    public int itemValue;
    public void Collect()
    {
        PlayerInventory inv = FindFirstObjectByType<PlayerInventory>();
        if(inv != null)
        {
            inv.ProcessPickup(this);
            //Destroy(gameObject);
        }
    }
}
