using UnityEngine;

public class npcController : MonoBehaviour, IInteractable
{
    [Header("NPC Info")]
    public string npcName;

    [TextArea(2, 5)]
    public string[] talkingLines;

    public void Interact(PlayerInteraction playerInteraction)
    {
        if (talkingManager.instance == null)
            return;

        talkingManager.instance.startDialogue(npcName, talkingLines);
    }
}
