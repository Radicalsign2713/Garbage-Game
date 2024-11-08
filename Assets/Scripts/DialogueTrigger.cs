using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager; // Reference to the DialogueManager
    public Dialogue dialogue; // Reference to the Dialogue ScriptableObject

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player object is tagged "Player"
        {
            dialogueManager.StartDialogue(dialogue);
        }
    }
}
