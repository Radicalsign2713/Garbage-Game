using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialogueTrigger : MonoBehaviour
{
    private DialogueManager manager;
    private PlayerMovementController player;
    private int dialogueStage = 0;

    // Assign these in the Inspector for each specific object that should trigger a dialogue segment
    public GameObject[] dialogueObjects;
    private bool[] hasTriggered;

    // Start is called before the first frame update
    void Start()
    {
        // Find the DialogueManager and PlayerMovementController components
        manager = GameObject.FindObjectOfType<DialogueManager>();
        player = GameObject.FindObjectOfType<PlayerMovementController>();
        
        // Freeze player movement at the start
        player.game_frozen = true;
        Time.timeScale = 0; // Pause game time
        
        // Initialize hasTriggered array based on the number of dialogue objects
        hasTriggered = new bool[dialogueObjects.Length];

        // Trigger the initial dialogue without interaction
        TriggerDialogueSegment();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < dialogueObjects.Length; i++)
            {
                if (!hasTriggered[i] && collision.gameObject == dialogueObjects[i])
                {
                    hasTriggered[i] = true;
                    TriggerDialogueSegment();
                    break;
                }
            }
        }
    }

    void TriggerDialogueSegment()
    {
        if (dialogueStage < dialogueObjects.Length)
        {
            manager.TriggerDialogue();
            dialogueStage++;
        }
        else
        {
            // Unfreeze player when all dialogues have been triggered
            player.game_frozen = false;
            Time.timeScale = 1; // Resume game time
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current dialogue has finished
        if (manager.isFinished && player.game_frozen)
        {
            // Allow player movement and advance to the next stage if needed
            player.game_frozen = false;
            Time.timeScale = 1; // Resume game time
        }
    }
}
