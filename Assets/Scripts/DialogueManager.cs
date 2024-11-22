using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text characterNameText;
    public Image characterLeftPortrait;
    public Image characterRightPortrait;
    public GameObject dialogueUI;
    public GameObject skipDialoguePanel;
    public TMP_Text skipSummaryText;
    public Button skipButton;
    public Button confirmSkipButton;
    public Button cancelSkipButton;

    public Image backgroundImage;
    public Image fadePanel;

    public float typingSpeed = 0.05f;
    public float bounceAmount = 5f;
    public float bounceSpeed = 2f;
    public float fadeDuration = 0.5f;

    public Dialogue testDialogue;

    private Coroutine typingCoroutine;
    private Coroutine portraitCoroutine;
    private Coroutine fadeCoroutine;
    private Vector3 initialLeftPortraitPosition;
    private Vector3 initialRightPortraitPosition;

    private Queue<Dialogue.DialogueLine> sentences;

    private Dialogue.DialogueLine currentLine;
    private Dialogue.CharacterSide lastSpeakingSide;

    private bool isTyping;
    private bool isFading;
    private bool isUnFading;

    public bool isFinished = false;

    void Awake()
    {
        initialLeftPortraitPosition = characterLeftPortrait.rectTransform.localPosition;
        initialRightPortraitPosition = characterRightPortrait.rectTransform.localPosition;

        sentences = new Queue<Dialogue.DialogueLine>();

        dialogueUI.SetActive(false);
        characterLeftPortrait.gameObject.SetActive(false);
        characterRightPortrait.gameObject.SetActive(false);
        skipDialoguePanel.SetActive(false);
        fadePanel.gameObject.SetActive(false);

        skipButton.onClick.AddListener(ShowSkipConfirmation);
        confirmSkipButton.onClick.AddListener(SkipDialogue);
        cancelSkipButton.onClick.AddListener(HideSkipConfirmation);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            if (isFading || isUnFading)
            {
                return; // Prevent any interaction while fading/un-fading is in progress
            }

            if (isTyping)
            {
                // If typing, complete the current line first
                FinishTyping();
            }
            else if (fadeCoroutine == null)
            {
                // Move to the next line if typing is finished
                DisplayNextSentence();
            }
        }
    }


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

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueUI.SetActive(true);
        skipButton.gameObject.SetActive(true);

        sentences.Clear();

        foreach (Dialogue.DialogueLine line in dialogue.lines)
        {
            sentences.Enqueue(line);
        }

        lastSpeakingSide = Dialogue.CharacterSide.Left;

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;
        }

        if (portraitCoroutine != null)
        {
            StopCoroutine(portraitCoroutine);
        }

        currentLine = sentences.Dequeue();

        // Fade transition before showing the new dialogue line
        if (currentLine.fadeToBlackBefore)
        {
            fadeCoroutine = StartCoroutine(FadeToBlackAndDisplay());
        }
        else if (currentLine.unFadeBefore)
        {
            fadeCoroutine = StartCoroutine(FadeFromBlackAndDisplay());
        }
        else
        {
            DisplayLineWithTransitions();
        }
    }

    private IEnumerator FadeToBlackAndDisplay()
    {
        isFading = true;

        fadePanel.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn(fadePanel, fadeDuration));

        DisplayLineWithTransitions();

        isFading = false;
        fadeCoroutine = null;
    }

    private IEnumerator FadeFromBlackAndDisplay()
    {
        isUnFading = true;

        fadePanel.gameObject.SetActive(true);
        yield return StartCoroutine(FadeOut(fadePanel, fadeDuration));

        fadePanel.gameObject.SetActive(false);
        DisplayLineWithTransitions();

        isUnFading = false;
        fadeCoroutine = null;
    }

    private void DisplayLineWithTransitions()
    {
        characterNameText.text = currentLine.characterName;

        if (currentLine.newBackground != null)
        {
            backgroundImage.sprite = currentLine.newBackground;
        }

        if (currentLine.side == Dialogue.CharacterSide.Left)
        {
            characterLeftPortrait.gameObject.SetActive(true);
            characterLeftPortrait.sprite = currentLine.characterPortrait;
            HighlightCharacter(characterLeftPortrait, initialLeftPortraitPosition);
            lastSpeakingSide = Dialogue.CharacterSide.Left;

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

            if (characterLeftPortrait.sprite == null)
            {
                characterLeftPortrait.gameObject.SetActive(false);
            }
            else
            {
                DimCharacter(characterLeftPortrait, initialLeftPortraitPosition);
            }
        }

        typingCoroutine = StartCoroutine(TypeSentence(currentLine.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        int charIndex = 0;
        bool insideTag = false;

        while (charIndex < sentence.Length)
        {
            if (sentence[charIndex] == '<') // Begin a tag
            {
                insideTag = true;
            }

            dialogueText.text += sentence[charIndex];
            charIndex++;

            if (sentence[charIndex - 1] == '>') // End a tag
            {
                insideTag = false;
            }

            if (!insideTag)
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        isTyping = false;
    }

    void FinishTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        isTyping = false;
        dialogueText.text = currentLine.sentence;
    }

    void HighlightCharacter(Image characterPortrait, Vector3 originalPosition)
    {
        characterPortrait.color = new Color(1f, 1f, 1f, 1f);
        characterPortrait.rectTransform.localPosition = originalPosition;
        portraitCoroutine = StartCoroutine(AnimateCharacterPortrait(characterPortrait, originalPosition));
    }

    void DimCharacter(Image characterPortrait, Vector3 originalPosition)
    {
        characterPortrait.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        characterPortrait.rectTransform.localPosition = originalPosition;
    }

    IEnumerator AnimateCharacterPortrait(Image characterPortrait, Vector3 originalPosition)
    {
        while (true)
        {
            characterPortrait.rectTransform.localPosition = originalPosition + Vector3.up * Mathf.Sin(Time.time * bounceSpeed) * bounceAmount;
            yield return null;
        }
    }

    IEnumerator FadeIn(Image panel, float duration)
    {
        float elapsedTime = 0;
        Color color = panel.color;
        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / duration);
            panel.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1;
        panel.color = color;
    }

    IEnumerator FadeOut(Image panel, float duration)
    {
        float elapsedTime = 0;
        Color color = panel.color;
        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / duration);
            panel.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 0;
        panel.color = color;
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        characterLeftPortrait.rectTransform.localPosition = initialLeftPortraitPosition;
        characterRightPortrait.rectTransform.localPosition = initialRightPortraitPosition;

        dialogueUI.SetActive(false);
        characterLeftPortrait.gameObject.SetActive(false);
        characterRightPortrait.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);

        isFinished = true;
    }

    void ShowSkipConfirmation()
    {
        skipDialoguePanel.SetActive(true);
        if (testDialogue != null)
        {
            skipSummaryText.text = testDialogue.dialogueSummary;
        }
    }

    void HideSkipConfirmation()
    {
        skipDialoguePanel.SetActive(false);
    }

    void SkipDialogue()
    {
        skipDialoguePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
