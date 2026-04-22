using UnityEngine;

public class DialogueTestScript : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    public string characterName;
    public string singleLine;
    public string[] multipleLines;
    public float delayBetweenLines = 4f;
    public float typingSpeed = 0.02f;

    private bool isTalking = false;

    public void Interact(PlayerInteraction playerInteraction)
    {
        if (dialogue == null)
            return;

        if (dialogue.isTalking)
            return;

        startDialogue();
    }

    private void startDialogue()
    {
        dialogue.typingSpeed = typingSpeed;
        isTalking = true;

        if (multipleLines == null || multipleLines.Length == 0)
        {
            dialogue.Say(singleLine, characterName, delayBetweenLines);
        }
        else
        {
            dialogue.Say(multipleLines, characterName, delayBetweenLines);
        }
    }



/*
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Skip();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Clear();
        }
    }
    */
/*
    public void startDialogue(Dialogue dialogue)
    {
        currentLines = dialogue.lines;
        currentLineIndex = 0;
        isTalking = true;

        dialoguePanel.SetActive(true);
        nameText.text = dialogue.npcName;
        dialogueText.text = currentLines[currentLineIndex];
    }
    */
}
