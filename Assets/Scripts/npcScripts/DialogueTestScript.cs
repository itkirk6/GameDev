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
        {
            Debug.Log("Dialogue is null - exiting");
            return;
        }

        if (dialogue.isTalking)
        {
            Debug.Log("Dialogue is already talking - exiting");
            return;
        }

        if (isQuestNpc)
        {
            Debug.Log("interacting with a quest NPC");
            PlayerInventory playerInventory = playerInteraction.GetComponent<PlayerInventory>();

            if (!questCompleted && playerInventory != null && playerHasRequiredItem(playerInventory))
            {
                Debug.Log("quest completed");
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
        //Debug.Log("starting normal doalogue");
        dialogue.typingSpeed = typingSpeed;

        if (multipleLines == null || multipleLines.Length == 0)
        {
            //Debug.Log("saying only one line?");
            dialogue.Say(singleLine, characterName, delayBetweenLines);
        }
        else
        {
            //Debug.Log("saying multiple lines");
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
        return playerInventory.questItems.Contains(requiredItemName);
    }

    private void completeQuest(PlayerInventory playerInventory)
    {
        if (playerInventory.questItems.Contains(requiredItemName))
        {
            playerInventory.questItems.Remove(requiredItemName);
        }

        Vector3 spawnPosition = transform.position;

        if (rewardDropPoint != null)
        {
            spawnPosition = rewardDropPoint.position;
        }

        if (rewardItemPrefab != null)
        {
            Instantiate(rewardItemPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
        }

        questCompleted = true;
    }
}
