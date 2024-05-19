import random
import time

# -----------------------------------------------------------------------------
# Data
# -----------------------------------------------------------------------------

drinks = {
    "Coffee": {"ingredients": ["Coffee Beans", "Hot Water"], "effect": "Energy boost"},
    "Tea": {"ingredients": ["Tea Leaves", "Hot Water"], "effect": "Relaxation"},
    "Space Latte": {"ingredients": ["Cosmic Dust", "Moon Milk", "Stardust"], "effect": "Cosmic Awareness"},
    "Hot Chocolate": {"ingredients": ["Cocoa Powder", "Milk", "Sugar"], "effect": "Warmth and Comfort"},
    "Iced Matcha": {"ingredients": ["Matcha Powder", "Ice", "Water"], "effect": "Focus and Clarity"}
}

customers = [
    {
        "name": "Aristotle",
        "time_period": "Ancient Greece",
        "problem": "Philosophical dilemma about existence",
        "desired_drink": "Tea",
        "dialogues": [
            "Greetings, wise barista. I seek solace for my troubled mind.",
            "Perhaps a calming tea would soothe my thoughts.",
            "Ah, this tea is just what I needed. Thank you, friend. This strange brew has given me much to ponder.",
            "This is not what I desired. My mind remains unsettled. Perhaps a different concoction would be more fitting."
        ]
    },
    {
        "name": "Cleopatra",
        "time_period": "Ancient Egypt",
        "problem": "Political intrigue and betrayal",
        "desired_drink": "Coffee",
        "dialogues": [
            "Barista, I require a beverage fit for a queen.",
            "A strong coffee, if you please. Something to awaken my senses and fortify my resolve.",
            "This coffee is divine! It invigorates me for the challenges ahead. You have my gratitude.",
            "This is not what I requested. Perhaps your knowledge of brewing is not as advanced as your time-traveling abilities."
        ]
    }
   
]

# -----------------------------------------------------------------------------
# Game Variables
# -----------------------------------------------------------------------------

current_customer = None
dialogue_history = []
has_ordered = False
rewinds_remaining = 3

# -----------------------------------------------------------------------------
# Functions
# -----------------------------------------------------------------------------

def main():
    global current_customer, dialogue_history, has_ordered, rewinds_remaining

    print("Welcome to Timeless Brews!")
    current_customer = random.choice(customers)
    print(f"\n{current_customer['name']} ({current_customer['time_period']}) enters the café.")

    while True:
        print("\nWhat would you like to do?")
        print("1. Talk to the customer")
        print("2. Make a drink")
        print("3. Rewind time ({} remaining)".format(rewinds_remaining))
        print("4. Quit")

        choice = input("> ")

        if choice == '1':
            if not has_ordered:
                dialogue_history.append(current_customer["dialogues"][0])
                print(current_customer["dialogues"][0])
            start_dialogue(current_customer, dialogue_history, has_ordered)
        elif choice == '2':
            if has_ordered:
                make_drink(current_customer, dialogue_history)
            else:
                print("The customer hasn't placed an order yet.")
        elif choice == '3':
            if len(dialogue_history) > 1 and rewinds_remaining > 0:
                dialogue_history = dialogue_history[:-1]
                rewinds_remaining -= 1
                print("\n*Time rewinds...*\n")
                print(dialogue_history[-1])
                has_ordered = False
            else:
                print("Cannot rewind further.")
        elif choice == '4':
            break
        else:
            print("Invalid choice.")

def start_dialogue(customer, dialogue_history, has_ordered):
    response_options = [
        "Inquire about their journey through time",
        "Ask about their problem",
        "Offer a drink"
    ]
    if has_ordered:
        response_options = [
            "Inquire about their time period",
            "Discuss their problem further",
            "Recommend a different drink"
        ]

    while True:
        print("\nHow would you like to respond?")
        for i, response in enumerate(response_options):
            print(f"{i+1}. {response}")

        choice = input("> ")

        try:
            choice_index = int(choice) - 1
            if 0 <= choice_index < len(response_options):
                response = response_options[choice_index]

                # Simple response logic
                if response == "Offer a drink" and not has_ordered:
                    dialogue_history.append(customer["dialogues"][1])  # Order dialogue
                    print(customer["dialogues"][1])
                    has_ordered = True
                    break
                elif has_ordered:
                    if response == "Inquire about their time period":
                        print(f"{customer['name']}:  I come from the time of {customer['time_period']}.")
                    elif response == "Discuss their problem further":
                        print(f"{customer['name']}: My problem is {customer['problem']}.")
                    elif response == "Recommend a different drink":
                        print("You: Perhaps you'd like to try something different?")
                        make_drink(customer, dialogue_history)
                else:
                    print(f"You: {response}")

            else:
                print("Invalid choice.")
        except ValueError:
            print("Invalid input. Please enter a number.")

def make_drink(customer, dialogue_history):
    print("\nAvailable ingredients:")
    for ingredient in drinks.keys():
        print(f"- {ingredient}")

    order = input("What would you like to make? ")

    if order in drinks:
        if all(ing in drinks[order]["ingredients"] for ing in customer["desired_drink"].split(",")):
            print(f"You made a perfect {order} for {customer['name']}!")
            print(customer["dialogues"][2])  # Correct drink response
        else:
            print(f"Oops! That wasn't the drink {customer['name']} wanted.")
            print(customer["dialogues"][3])  # Wrong drink response
    else:
        print("Sorry, we don't have that drink on the menu.")

if __name__ == "__main__":
    main()
