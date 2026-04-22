using UnityEngine;

public class DialogueTestScript : MonoBehaviour, IInteractable
{
    [Header("Dialogue")]
    public Dialogue dialogue;
    public string characterName;
    public string singleLine;
    public string[] multipleLines;
    public string[] completedLines;
    public float delayBetweenLines = 4f;
    public float typingSpeed = 0.02f;

    [Header("Quest")]
    public bool isQuestNpc = false;
    public string requiredItemName;
    public GameObject rewardItemPrefab;
    public Transform rewardDropPoint;

    private bool questCompleted = false;

    public void Interact(PlayerInteraction playerInteraction)
    {
        if (dialogue == null)
            return;

        if (dialogue.isTalking)
            return;

        if (isQuestNpc)
        {
            PlayerInventory playerInventory = playerInteraction.GetComponent<PlayerInventory>();

            if (!questCompleted && playerInventory != null && playerHasRequiredItem(playerInventory))
            {
                completeQuest(playerInventory);
                startDialogue(completedLines);
                return;
            }

            if (questCompleted)
            {
                startDialogue(completedLines);
                return;
            }
        }

        startNormalDialogue();
    }

    private void startNormalDialogue()
    {
        dialogue.typingSpeed = typingSpeed;

        if (multipleLines == null || multipleLines.Length == 0)
        {
            dialogue.Say(singleLine, characterName, delayBetweenLines);
        }
        else
        {
            dialogue.Say(multipleLines, characterName, delayBetweenLines);
        }
    }

    private void startDialogue(string[] lines)
    {
        if (lines == null || lines.Length == 0)
            return;

        dialogue.typingSpeed = typingSpeed;
        dialogue.Say(lines, characterName, delayBetweenLines);
    }

    private bool playerHasRequiredItem(PlayerInventory playerInventory)
    {
        if (playerInventory.currentEquippedItem == null)
            return false;

        ItemPickup heldItemPickup = playerInventory.currentEquippedItem.GetComponent<ItemPickup>();

        if (heldItemPickup == null)
            return false;

        return heldItemPickup.itemName == requiredItemName;
    }

    private void completeQuest(PlayerInventory playerInventory)
    {
        GameObject heldItem = playerInventory.currentEquippedItem;
        playerInventory.currentEquippedItem = null;

        if (playerInventory.equippedItemUIBox != null)
        {
            playerInventory.equippedItemUIBox.sprite = null;
            playerInventory.equippedItemUIBox.enabled = false;
        }

        if (heldItem != null)
        {
            Destroy(heldItem);
        }

        Vector3 spawnPosition = transform.position;

        if (rewardDropPoint != null)
        {
            spawnPosition = rewardDropPoint.position;
        }

        if (rewardItemPrefab != null)
        {
            Instantiate(rewardItemPrefab, spawnPosition, Quaternion.identity);
        }

        questCompleted = true;
    }
}
