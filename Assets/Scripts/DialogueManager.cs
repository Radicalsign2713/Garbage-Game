using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Required for scene management

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text characterNameText;
    public Image characterLeftPortrait; // Portrait for the character on the left
    public Image characterRightPortrait; // Portrait for the character on the right
    public GameObject dialogueUI; // Reference to the dialogue UI panel
    public GameObject skipDialoguePanel; // UI panel for skip confirmation
    public TMP_Text skipSummaryText; // Text for skip summary
    public Button skipButton; // Button to trigger skip confirmation
    public Button confirmSkipButton; // Button to confirm skipping dialogue
    public Button cancelSkipButton; // Button to cancel skipping dialogue

    public float typingSpeed = 0.05f;
    public float bounceAmount = 5f; // Amount of movement for the hop
    public float bounceSpeed = 2f;  // Speed of the hop

    public Dialogue testDialogue; // Placeholder for testing, assign it in the Inspector

    private Coroutine typingCoroutine;
    private Coroutine portraitCoroutine; // Reference to the current animation coroutine
    private Vector3 initialLeftPortraitPosition;
    private Vector3 initialRightPortraitPosition;

    private Queue<Dialogue.DialogueLine> sentences; // Stores dialogue lines

    private Dialogue.DialogueLine currentLine; // Stores the current dialogue line
    private Dialogue.CharacterSide lastSpeakingSide; // To keep track of who spoke last

    private bool isTyping; // Flag to check if typing is in progress

    public bool isFinished = false;

    void Awake()
    {
        // Store the original position of both character portraits
        initialLeftPortraitPosition = characterLeftPortrait.rectTransform.localPosition;
        initialRightPortraitPosition = characterRightPortrait.rectTransform.localPosition;

        // Initialize the sentences queue
        sentences = new Queue<Dialogue.DialogueLine>();

        // Hide the dialogue UI and portraits at the start
        dialogueUI.SetActive(false);
        characterLeftPortrait.gameObject.SetActive(false);
        characterRightPortrait.gameObject.SetActive(false);
        skipDialoguePanel.SetActive(false);

        // Add listeners to buttons
        skipButton.onClick.AddListener(ShowSkipConfirmation);
        confirmSkipButton.onClick.AddListener(SkipDialogue);
        cancelSkipButton.onClick.AddListener(HideSkipConfirmation);
    }

    void Update()
    {
        // Check if the player presses Space, Enter, or Left Click to proceed through the dialogue
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // If typing is in progress, finish typing the current line
                FinishTyping();
            }
            else
            {
                // If typing is not in progress, display the next sentence
                DisplayNextSentence();
            }
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
            Debug.LogWarning("No test dialogue assigned in DialogueManager."); // Keeping this warning for debugging purposes
        }
    }

    // Method to start a new dialogue using the provided Dialogue ScriptableObject
    public void StartDialogue(Dialogue dialogue)
    {
        // Show the dialogue UI
        dialogueUI.SetActive(true);
        skipButton.gameObject.SetActive(true);

        // Clear out any existing sentences
        sentences.Clear();

        // Add new dialogue lines to the queue
        foreach (Dialogue.DialogueLine line in dialogue.lines)
        {
            sentences.Enqueue(line);
        }

        // Reset the last speaking side since we're starting a new dialogue
        lastSpeakingSide = Dialogue.CharacterSide.Left; // Or you can set it to a neutral/default value

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

        // Stop any ongoing portrait animation coroutine
        if (portraitCoroutine != null)
        {
            StopCoroutine(portraitCoroutine);
        }

        // Get the next dialogue line
        currentLine = sentences.Dequeue();

        // Update character name in UI
        characterNameText.text = currentLine.characterName;

        // Highlight the active character based on which side they are on
        if (currentLine.side == Dialogue.CharacterSide.Left)
        {
            characterLeftPortrait.gameObject.SetActive(true);
            characterLeftPortrait.sprite = currentLine.characterPortrait;
            HighlightCharacter(characterLeftPortrait, initialLeftPortraitPosition);
            lastSpeakingSide = Dialogue.CharacterSide.Left;

            // Hide or reset the right character
            if (characterRightPortrait.sprite == null)
            {
                characterRightPortrait.gameObject.SetActive(false);
            }
            else
            {
                DimCharacter(characterRightPortrait, initialRightPortraitPosition);
            }
        }
        else if (currentLine.side == Dialogue.CharacterSide.Right)
        {
            characterRightPortrait.gameObject.SetActive(true);
            characterRightPortrait.sprite = currentLine.characterPortrait;
            HighlightCharacter(characterRightPortrait, initialRightPortraitPosition);
            lastSpeakingSide = Dialogue.CharacterSide.Right;

            // Hide or reset the left character
            if (characterLeftPortrait.sprite == null)
            {
                characterLeftPortrait.gameObject.SetActive(false);
            }
            else
            {
                DimCharacter(characterLeftPortrait, initialLeftPortraitPosition);
            }
        }

        // Start typing the dialogue sentence
        typingCoroutine = StartCoroutine(TypeSentence(currentLine.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        // Convert any markdown-like syntax for bold into TMP rich text tags
        string parsedSentence = sentence.Replace("**", "<b>");

        // Use a flag to determine if we are in the middle of a rich text tag
        bool inTag = false;

        for (int i = 0; i < parsedSentence.Length; i++)
        {
            char currentChar = parsedSentence[i];

            // Check if we are entering or exiting a rich text tag
            if (currentChar == '<')
            {
                inTag = true;
            }

            // Append the current character
            dialogueText.text += currentChar;

            // If we are at the end of a tag
            if (currentChar == '>')
            {
                inTag = false;
            }

            // If not in a tag, wait before adding the next character
            if (!inTag)
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // Ensure the final text is displayed correctly at the end
        dialogueText.text = parsedSentence;

        isTyping = false;
    }



    void FinishTyping()
    {
        // Finish typing the current sentence immediately
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        isTyping = false;
        dialogueText.text = currentLine.sentence;
    }

    void HighlightCharacter(Image characterPortrait, Vector3 originalPosition)
    {
        // Set the character to full brightness and start the hop animation
        characterPortrait.color = new Color(1f, 1f, 1f, 1f); // Full brightness

        // Start the hop animation only if it's not already running
        characterPortrait.rectTransform.localPosition = originalPosition; // Reset to original position
        portraitCoroutine = StartCoroutine(AnimateCharacterPortrait(characterPortrait, originalPosition));
    }

    void DimCharacter(Image characterPortrait, Vector3 originalPosition)
    {
        // Dim the character and stop any bounce animation
        characterPortrait.color = new Color(0.5f, 0.5f, 0.5f, 1f); // Dimmed brightness
        characterPortrait.rectTransform.localPosition = originalPosition; // Ensure it goes back to original position
    }

    IEnumerator AnimateCharacterPortrait(Image characterPortrait, Vector3 originalPosition)
    {
        while (true)
        {
            // Make the character sprite hop up and down based on original position
            characterPortrait.rectTransform.localPosition = originalPosition + Vector3.up * Mathf.Sin(Time.time * bounceSpeed) * bounceAmount;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        // Stop the character portrait animation and reset their positions
        StopAllCoroutines();
        characterLeftPortrait.rectTransform.localPosition = initialLeftPortraitPosition;
        characterRightPortrait.rectTransform.localPosition = initialRightPortraitPosition;

        // Hide the dialogue UI, portraits, and skip button
        dialogueUI.SetActive(false);
        characterLeftPortrait.gameObject.SetActive(false);
        characterRightPortrait.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);

        isFinished = true;
    }

    // Show the skip confirmation panel
    void ShowSkipConfirmation()
    {
        skipDialoguePanel.SetActive(true);
        if (testDialogue != null)
        {
            skipSummaryText.text = testDialogue.dialogueSummary; // Use the summary from the dialogue object
        }
    }

    // Hide the skip confirmation panel
    void HideSkipConfirmation()
    {
        skipDialoguePanel.SetActive(false);
    }

    // Skip the current dialogue and load the next scene
    void SkipDialogue()
    {
        skipDialoguePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
