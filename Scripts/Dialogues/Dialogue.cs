using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueLine[] lines;
}

[System.Serializable]
public class DialogueLine
{
    public string speakerName; // "Barista" or customer name
    public string text;
    public DialogueChoice[] choices; // Possible player responses (can be empty)
}

[System.Serializable]
public class DialogueChoice
{
    public string text;
    public int nextLineIndex; // Index of the next dialogue line to display
}