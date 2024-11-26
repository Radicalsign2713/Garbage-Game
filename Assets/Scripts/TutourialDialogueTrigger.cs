using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialogueTriggerForObjects : MonoBehaviour
{
    public Dialogue dialogueToTrigger; // Drag and drop the desired dialogue for each object
    private DialogueManager manager;
    private PlayerMovementController player;
    private bool hasTriggered = false;

    // Track whether the initial dialogue is triggered
    private bool initialDialoguePlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        // Find the DialogueManager and PlayerMovementController components
        manager = FindObjectOfType<DialogueManager>();
        player = FindObjectOfType<PlayerMovementController>();

        if (manager == null)
        {
            Debug.LogError("DialogueManager not found in the scene.");
            return;
        }

        if (player == null)
        {
            Debug.LogError("PlayerMovementController not found in the scene.");
            return;
        }

        // Initially freeze player movement until the initial dialogue is triggered
        player.game_frozen = true;

        // Trigger initial dialogue at the start
        TriggerInitialDialogue();
    }

    private void TriggerInitialDialogue()
    {
        // Trigger the initial dialogue once
        if (!initialDialoguePlayed)
        {
            manager.TriggerDialogue();
            initialDialoguePlayed = true;

            // Wait for the initial dialogue to complete before unfreezing the player
            StartCoroutine(WaitForDialogueToEnd());
        }
    }

    private IEnumerator WaitForDialogueToEnd()
    {
        // Wait until the dialogue manager marks the dialogue as finished
        yield return new WaitUntil(() => manager.isFinished);

        // Unfreeze player movement and reset isFinished for subsequent dialogues
        player.game_frozen = false;
        manager.isFinished = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            if (dialogueToTrigger != null)
            {
                // Trigger the specified dialogue when the player interacts with this object
                manager.StartDialogue(dialogueToTrigger);

                // Freeze player movement during dialogue
                player.game_frozen = true;

                // Mark as triggered so it won't trigger again
                hasTriggered = true;

                // Wait for this specific dialogue to end before allowing player movement
                StartCoroutine(WaitForDialogueToEnd());
            }
            else
            {
                Debug.LogWarning("No dialogue assigned to this object: " + gameObject.name);
            }
        }
    }
}
