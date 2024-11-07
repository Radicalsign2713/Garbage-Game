using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public string characterName; // Name of the speaking character
        public Sprite characterPortrait; // Character portrait for the dialogue line
        [TextArea(3, 10)] public string sentence; // Dialogue text
    }

    public DialogueLine[] lines; // Array of dialogue lines
}
