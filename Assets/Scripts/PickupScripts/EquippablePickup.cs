using UnityEngine;

public class EquippablePickup : MonoBehaviour, IInteractable
{
    public GameObject itemPrefab;

    public void Interact(PlayerInteraction player)
    {
        player.EquipItem(itemPrefab);
        Destroy(gameObject);
    }
}