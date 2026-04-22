using UnityEngine;
using TMPro;

public class talkingManager : MonoBehaviour
{
    public static talkingManager instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private string[] currentLines;
    private int currentLineIndex;
    private bool isTalking;
    private bool canAdvance = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!isTalking || !canAdvance)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            showNextLine();
        }
    }

    public void startDialogue(string npcName, string[] lines)
    {
        if (lines == null || lines.Length == 0)
            return;

        currentLines = lines;
        currentLineIndex = 0;
        isTalking = true;
        canAdvance = false;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        if (nameText != null)
            nameText.text = npcName;

        if (dialogueText != null)
            dialogueText.text = currentLines[currentLineIndex];
        
        Invoke(nameof(enableAdvance), 0.1f);
    }

    public void showNextLine()
    {
        if (!isTalking)
            return;

        currentLineIndex++;

        if (currentLineIndex >= currentLines.Length)
        {
            endTalking();
            return;
        }

        if (dialogueText != null)
            dialogueText.text = currentLines[currentLineIndex];
    }

    public void endTalking()
    {
        isTalking = false;
        currentLines = null;
        currentLineIndex = 0;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    public bool getIsDialogueActive()
    {
        return isTalking;
    }
    private void enableAdvance()
    {
        canAdvance = true;
    }
}