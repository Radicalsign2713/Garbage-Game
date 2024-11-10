using System.Collections;
using System.Collections.Generic; // This is needed for Queue<string>
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text characterNameText;
    public Image characterPortrait; // This is the character sprite you want to animate
    public GameObject dialogueUI; // Reference to the dialogue UI panel

    public float typingSpeed = 0.05f;
    public float bounceAmount = 5f; // Amount of movement for the hop
    public float bounceSpeed = 2f;  // Speed of the hop

    public Dialogue testDialogue; // Placeholder for testing, assign it in the Inspector

    private Coroutine typingCoroutine;
    private Vector3 initialPortraitPosition;

    private Queue<Dialogue.DialogueLine> sentences; // Stores dialogue lines

    void Start()
    {
        // Store the original position of the character portrait
        initialPortraitPosition = characterPortrait.rectTransform.localPosition;

        // Initialize the sentences queue
        sentences = new Queue<Dialogue.DialogueLine>();

        // Hide the dialogue UI at the start
        dialogueUI.SetActive(false);
    }

    void Update()
    {
        // Check if the player presses the D key to trigger dialogue (for testing)
        if (Input.GetKeyDown(KeyCode.D))
        {
            TriggerDialogue();
        }

        // Check if the player presses Space, Enter, or Left Click to proceed through the dialogue
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    // Method to trigger dialogue manually or by receiving a signal
    public void TriggerDialogue()
    {
        if (testDialogue != null)
        {
            StartDialogue(testDialogue);
        }
        else
        {
            Debug.LogWarning("No test dialogue assigned in DialogueManager.");
        }
    }

    // Method to start a new dialogue using the provided Dialogue ScriptableObject
    public void StartDialogue(Dialogue dialogue)
    {
        // Show the dialogue UI
        dialogueUI.SetActive(true);

        // Clear out any existing sentences
        sentences.Clear();

        // Add new dialogue lines to the queue
        foreach (Dialogue.DialogueLine line in dialogue.lines)
        {
            sentences.Enqueue(line);
        }

        // Start typing the first line
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // If no sentences are left, end the dialogue
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Stop previous typing coroutine, if any
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // Get the next dialogue line
        Dialogue.DialogueLine line = sentences.Dequeue();
        
        // Update character name and portrait
        characterNameText.text = line.characterName;
        characterPortrait.sprite = line.characterPortrait;

        // Type out the dialogue sentence
        typingCoroutine = StartCoroutine(TypeSentence(line.sentence));

        // Start the animation of hopping up and down
        StartCoroutine(AnimateCharacterPortrait());
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator AnimateCharacterPortrait()
    {
        while (true)
        {
            // Make the character sprite hop up and down
            characterPortrait.rectTransform.localPosition = initialPortraitPosition + Vector3.up * Mathf.Sin(Time.time * bounceSpeed) * bounceAmount;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        // Stop the character portrait animation and reset its position
        StopAllCoroutines();
        characterPortrait.rectTransform.localPosition = initialPortraitPosition;

        // Hide the dialogue UI
        dialogueUI.SetActive(false);
    }
}
