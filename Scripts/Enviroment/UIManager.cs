using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    // Dialogue UI
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button[] choiceButtons;

    // Drink Making UI
    public GameObject drinkMenuPanel;
    public Transform ingredientButtonContainer;
    public Button makeDrinkButton;
    public TextMeshProUGUI selectedIngredientsText;

    // Time Manipulation UI
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI rewindCountText;

    // Other UI elements
    public TextMeshProUGUI customerMoodText; // Example mood indicator

    private PlayerController playerController;
    private DrinkMaker drinkMaker;
    private DialogueManager dialogueManager;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        drinkMaker = FindObjectOfType<DrinkMaker>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        // Initialize UI elements
        dialoguePanel.SetActive(false);
        drinkMenuPanel.SetActive(false);

        // Update UI elements based on initial state
        UpdateTimeDisplay();
        UpdateRewindCount();
        UpdateCustomerMood(); 
    }

    private void Update()
    {
        // Update UI elements that need to change dynamically
        UpdateTimeDisplay();
        UpdateRewindCount();
        UpdateCustomerMood(); 
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        dialoguePanel.SetActive(true);
        dialogueManager.StartDialogue(dialogue);
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void ShowDrinkMenu()
    {
        drinkMenuPanel.SetActive(true);
    }

    public void HideDrinkMenu()
    {
        drinkMenuPanel.SetActive(false);
    }

    private void UpdateTimeDisplay()
    {
        // In-game time logic (you'll need to implement this based on your game's time system)
        string formattedTime = "18:00"; // Example time
        timeText.text = "Time: " + formattedTime;
    }

    private void UpdateRewindCount()
    {
        rewindCountText.text = "Rewinds: " + playerController.rewindsRemaining;
    }

    private void UpdateCustomerMood()
    {
        // Get the mood of the current customer
        string mood = "Neutral"; // Default mood, or get it from the current customer
        customerMoodText.text = "Mood: " + mood;
    }
}
