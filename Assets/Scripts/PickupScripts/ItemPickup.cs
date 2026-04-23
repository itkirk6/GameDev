using UnityEngine;

public class ItemPickup : MonoBehaviour, IPickup
{
    public enum PickupType { Equippable, Key, Consumable, QuestItem }

    public string itemName;
    public PickupType itemType;
    public int itemValue;

    public bool addToKeys = false;
    public bool addToQuestItems = false;

    public void Collect()
    {
        PlayerInventory inv = FindFirstObjectByType<PlayerInventory>();

        if (inv != null)
        {
            inv.ProcessPickup(this);
        }
    }

    public void pickup(PlayerInteraction playerInteraction)
    {
        Collect();
    }
}
