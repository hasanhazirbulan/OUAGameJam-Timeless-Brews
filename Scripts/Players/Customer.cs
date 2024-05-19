using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour
{
    public Dialogue initialDialogue;
    public Dialogue orderDialogue; // Dialogue when the customer is ready to order
    public Drink desiredDrink;
    public string mood;
    public SpriteRenderer spriteRenderer;
    public Sprite[] moodSprites;

    private bool hasOrdered = false;
    private bool isSatisfied = false;

    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>(); // Get the DialogueManager

        // Initialize mood-related visuals if using them
        if (spriteRenderer != null && moodSprites != null)
        {
            SetMoodSprite(mood);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasOrdered && other.CompareTag("Player"))
        {
            dialogueManager.StartDialogue(initialDialogue);
            hasOrdered = true;
        }
    }

    public void StartOrderDialogue()
    {
        // This function is called from the initial dialogue's last choice
        dialogueManager.StartDialogue(orderDialogue);
    }

    public void PlaceOrder()
    {
        // This function is called from the order dialogue's last choice
        Debug.Log("Customer ordered: " + desiredDrink.drinkName); // Replace with your drink making logic
    }

    public void ServeDrink(Drink servedDrink)
    {
        if (servedDrink == desiredDrink)
        {
            isSatisfied = true;
            Debug.Log("Customer is satisfied!");
            // Change mood (optional), play animation, etc.
        }
        else
        {
            // Wrong drink served
            Debug.Log("Customer is not happy...");
            // Change mood (optional), play animation, etc.
        }

        // After serving the drink, end the dialogue
        StartCoroutine(EndDialogueAfterDelay(2f)); // Wait 2 seconds before ending
    }

    private IEnumerator EndDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueManager.EndDialogue();
    }

    private void SetMoodSprite(string newMood)
    {
        // Logic to find and set the appropriate sprite based on newMood (e.g., using a dictionary or switch statement)
        // Example:
        // spriteRenderer.sprite = moodSprites[moodIndex(newMood)];
    }
}
