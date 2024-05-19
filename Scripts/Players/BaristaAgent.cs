using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class BaristaAgent : Agent
{
    public DrinkMaker drinkMaker;
    public Customer currentCustomer;
    public float rewardPerCorrectIngredient = 0.1f;
    public float penaltyPerIncorrectIngredient = -0.2f;
    public float rewardForCorrectDrink = 1.0f;
    public float penaltyForIncorrectDrink = -0.5f;

    public override void OnEpisodeBegin()
    {
        // Reset the environment (new customer, reset drink state)
        // ... (Logic to select a random customer and assign a desired drink)
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Observe customer's desired drink and available ingredients
        sensor.AddObservation(currentCustomer.desiredDrink.ingredients.Length); // Number of ingredients in the desired drink
        foreach (string ingredient in drinkMaker.availableDrinks[0].ingredients)
        {
            sensor.AddObservation(currentCustomer.desiredDrink.ingredients.Contains(ingredient) ? 1 : 0); // Is this ingredient needed?
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int ingredientIndex = actions.DiscreteActions[0]; 

        if (ingredientIndex < drinkMaker.availableDrinks[0].ingredients.Length)
        {
            drinkMaker.AddIngredient(drinkMaker.availableDrinks[0].ingredients[ingredientIndex]);
            AddReward(currentCustomer.desiredDrink.ingredients.Contains(drinkMaker.availableDrinks[0].ingredients[ingredientIndex])
                      ? rewardPerCorrectIngredient
                      : penaltyPerIncorrectIngredient);
        }
        else
        {
            // The action is to make the drink
            drinkMaker.MakeDrink();

            if (drinkMaker.lastMadeDrink == currentCustomer.desiredDrink)
            {
                AddReward(rewardForCorrectDrink);
                EndEpisode(); // Successful drink, end the episode
            }
            else
            {
                AddReward(penaltyForIncorrectDrink);
                // You might choose to end the episode here or give the agent another chance
            }
        }
    }
}
