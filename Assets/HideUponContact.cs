using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnContact : MonoBehaviour
{
    private PlayerMovementController player;
    private DialogueManager dialogueManager;

    void Start()
    {
        // Find the PlayerMovementController and DialogueManager in the scene
        player = FindObjectOfType<PlayerMovementController>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (player == null)
        {
            Debug.LogError("PlayerMovementController not found in the scene.");
        }

        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player has collided with the object, hiding the object now.");

            // Hide the object by making it invisible and disabling its collider
            GetComponent<SpriteRenderer>().enabled = false; // Make it invisible
            GetComponent<Collider2D>().enabled = false; // Disable collider to prevent further interactions

            // The player will be frozen by the dialogue UI visibility check in Update()
        }
    }

    void Update()
    {
        if (dialogueManager != null && player != null)
        {
            // Freeze player movement if the dialogue UI is active
            if (dialogueManager.dialogueUI.activeSelf)
            {
                player.game_frozen = true;
            }
            else
            {
                // Unfreeze player movement if the dialogue UI is not active
                player.game_frozen = false;
            }
        }
    }
}
