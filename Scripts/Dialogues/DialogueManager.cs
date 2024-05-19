using UnityEngine;
using UnityEngine.UI;
using TMPro;  // For TextMeshPro components

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button[] choiceButtons;

    private Dialogue currentDialogue;
    private int currentLineIndex = 0;
    public bool DialogueActive { get; private set; } = false;

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        DialogueActive = true;
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (currentLineIndex < currentDialogue.lines.Length)
        {
            DialogueLine line = currentDialogue.lines[currentLineIndex];
            nameText.text = line.speakerName;
            dialogueText.text = line.text;

            for (int i = 0; i < choiceButtons.Length; i++)
            {
                choiceButtons[i].gameObject.SetActive(i < line.choices.Length);
                if (i < line.choices.Length)
                {
                    choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = line.choices[i].text;
                    int choiceIndex = i; // Local copy for lambda
                    choiceButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners
                    choiceButtons[i].onClick.AddListener(() => MakeChoice(choiceIndex));
                }
            }
        }
        else
        {
            EndDialogue();
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentLineIndex = currentDialogue.lines[currentLineIndex].choices[choiceIndex].nextLineIndex;
        DisplayNextLine();
    }

    public void EndDialogue()
    {
        DialogueActive = false;
        // Disable/hide dialogue UI elements here
    }
}

