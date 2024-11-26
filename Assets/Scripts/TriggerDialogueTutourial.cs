using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueTutourial : MonoBehaviour
{
    private DialogueManager manager;
    private PlayerMovementController player;

    // Start is called before the first frame update
    void Start()
    {
        // Get the DialogueManager component in the scene
        manager = FindObjectOfType<DialogueManager>();

        if (manager == null)
        {
            Debug.LogError("DialogueManager not found in the scene.");
            return;
        }

        // Get the PlayerMovementController component
        player = FindObjectOfType<PlayerMovementController>();

        if (player == null)
        {
            Debug.LogError("PlayerMovementController not found in the scene.");
            return;
        }

        // Trigger the initial dialogue
        manager.TriggerDialogue();

        // Freeze player movement
        player.game_frozen = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the dialogue has finished
        if (manager.isFinished)
        {
            // Unfreeze player movement
            player.game_frozen = false;

            // Ensure this script does not try to unfreeze multiple times
            this.enabled = false; // Disable this script once the dialogue has ended
        }
    }
}
