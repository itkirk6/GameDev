using UnityEngine;

public class ConsumablePickup : MonoBehaviour, IInteractable
{
    public int healthRestoreAmount = 25;

    public void Interact(PlayerInteraction player)
    {
        //TODO: 
        Destroy(gameObject);
    }
}