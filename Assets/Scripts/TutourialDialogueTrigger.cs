using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialogueTrigger1 : MonoBehaviour
{
    public Dialogue dialogue; // Assign different dialogues in the inspector for each object
    private DialogueManager manager;
    private PlayerMovementController player;
    private bool hasTriggered = false;

    void Start()
    {
        // Find the DialogueManager and PlayerMovementController components
        manager = GameObject.FindObjectOfType<DialogueManager>();
        player = GameObject.FindObjectOfType<PlayerMovementController>();

        // Freeze player movement and time at the start of the scene
        player.game_frozen = true;
        Time.timeScale = 0f;

        // Start the dialogue for the tutorial introduction (if any)
        if (dialogue != null)
        {
            manager.TriggerDialogue();
            manager.StartDialogue(dialogue);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            // Trigger the specific dialogue for this object
            if (dialogue != null)
            {
                manager.TriggerDialogue();
                manager.StartDialogue(dialogue);
            }

            // Freeze player movement and pause the game time during dialogue
            player.game_frozen = true;
            Time.timeScale = 0f;

            // Mark as triggered so it won't trigger again
            hasTriggered = true;

            // Destroy this object after the dialogue is triggered
            StartCoroutine(DestroyAfterDialogue());
        }
    }

    // Coroutine to destroy the object after dialogue finishes
    IEnumerator DestroyAfterDialogue()
    {
        // Wait until the dialogue is finished
        while (!manager.isFinished)
        {
            yield return null;
        }

        // Allow the player to move again and resume time
        player.game_frozen = false;
        Time.timeScale = 1f;

        // Destroy the object
        Destroy(gameObject);
    }

    void Update()
    {
        // Check if the dialogue has finished (for initial tutorial dialogue)
        if (manager.isFinished && hasTriggered == false)
        {
            // Allow the player to move and resume time after initial tutorial dialogue
            player.game_frozen = false;
            Time.timeScale = 1f;
        }
    }
}
