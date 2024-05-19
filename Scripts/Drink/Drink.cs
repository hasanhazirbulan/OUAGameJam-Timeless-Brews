using UnityEngine;

[CreateAssetMenu(fileName = "New Drink", menuName = "Drink")]
public class Drink : ScriptableObject
{
    public string drinkName;
    public Sprite icon;
    public string[] ingredients; 
    public string effect; 
}