using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // For TextMeshPro components (if you're using them)

public class DrinkMaker : MonoBehaviour
{
    public List<Drink> availableDrinks; // List of ScriptableObject drinks
    public Transform ingredientButtonContainer; // Container for ingredient buttons
    public Button makeDrinkButton;
    public TextMeshProUGUI selectedIngredientsText; // UI element to display selected ingredients

    private List<string> selectedIngredients = new List<string>();

    private void Start() 
    {
        // Create ingredient buttons dynamically from availableDrinks
        foreach (Drink drink in availableDrinks)
        {
            foreach (string ingredient in drink.ingredients)
            {
                if (!selectedIngredients.Contains(ingredient))
                {
                    CreateIngredientButton(ingredient);
                }
            }
        }
    }

    private void CreateIngredientButton(string ingredientName)
    {
        GameObject buttonObject = new GameObject(ingredientName, typeof(Button), typeof(Image));
        buttonObject.transform.SetParent(ingredientButtonContainer, false); // Add to container
        Button button = buttonObject.GetComponent<Button>();
        
        // Customize the button's appearance (text, color, etc.)
        TextMeshProUGUI buttonText = buttonObject.AddComponent<TextMeshProUGUI>();
        buttonText.text = ingredientName;
        
        // Add a listener to the button
        button.onClick.AddListener(() => AddIngredient(ingredientName));
    }

    public void AddIngredient(string ingredientName)
    {
        selectedIngredients.Add(ingredientName);
        UpdateSelectedIngredientsText();
    }

    private void UpdateSelectedIngredientsText()
    {
        selectedIngredientsText.text = "Selected Ingredients: " + string.Join(", ", selectedIngredients);
    }

    public void MakeDrink()
    {
        Drink matchingDrink = null;
        foreach (Drink drink in availableDrinks)
        {
            if (new HashSet<string>(drink.ingredients).SetEquals(selectedIngredients))
            {
                matchingDrink = drink;
                break;
            }
        }

        if (matchingDrink != null)
        {
            Debug.Log("Made drink: " + matchingDrink.drinkName);
            // Apply drink effect (if any)
        }
        else
        {
            Debug.Log("Invalid combination of ingredients");
            // Display error message to the player
        }

        selectedIngredients.Clear();
        UpdateSelectedIngredientsText();
    }
}
