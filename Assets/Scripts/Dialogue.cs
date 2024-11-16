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
    }

    public DialogueLine[] lines; // Array of all dialogue lines

    // Enum to define which side the character is on
    public enum CharacterSide
    {
        Left,
        Right
    }

    // Add the dialogue summary for skip dialogue functionality
    [TextArea(3, 10)]
    public string dialogueSummary; // Summary of the dialogue, used in skip confirmation panel
}
