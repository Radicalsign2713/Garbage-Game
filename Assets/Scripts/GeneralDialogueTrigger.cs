using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDialogueTrigger : MonoBehaviour
{
    private DialogueManager manager;
    private PlayerMovementController player;
    private bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        // Find the DialogueManager and PlayerMovementController components
        manager = GameObject.FindObjectOfType<DialogueManager>();
        player = GameObject.FindObjectOfType<PlayerMovementController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            // Trigger the dialogue
            manager.TriggerDialogue();

            // Freeze player movement during dialogue
            player.game_frozen = true;

            // Mark as triggered so it won't trigger again
            hasTriggered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the dialogue has finished
        if (manager.isFinished)
        {
            // Allow the player to move again
            player.game_frozen = false;
        }
    }
}
