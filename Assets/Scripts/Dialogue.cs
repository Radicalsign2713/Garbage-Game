using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public string characterName; // Character's actual name to show in UI
        public Sprite characterPortrait; // Character's portrait
        public string sentence; // The sentence they speak
        public CharacterSide side; // Which side they are on (left or right)
        
        // New fields
        public bool fadeToBlackBefore; // Should fade to black before this line?
        public bool unFadeBefore; // Should un-fade before this line?
        public Sprite newBackground; // Optional new background for this line
    }

    public string dialogueSummary; // Summary of the dialogue for the skip option
    public DialogueLine[] lines; // Array of all dialogue lines

    // Enum to define which side the character is on
    public enum CharacterSide
    {
        Left,
        Right
    }
}
