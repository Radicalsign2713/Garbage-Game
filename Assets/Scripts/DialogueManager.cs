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
    public Image secondaryBackgroundImage; // Used for cross-fading backgrounds
    public Image fadePanel; // Panel used for fade-to-black transition

    public float typingSpeed = 0.05f;
    public float bounceAmount = 5f;
    public float bounceSpeed = 2f;
    public float fadeDuration = 0.5f;

    public Dialogue testDialogue;

    // Music playback
    public AudioSource audioSource;
    public AudioClip introMusicClip;
    public AudioClip loopMusicClip;

    private Coroutine typingCoroutine;
    private Coroutine portraitCoroutine;
    private Coroutine fadeCoroutine;
    private Coroutine backgroundFadeCoroutine;
    private Vector3 initialLeftPortraitPosition;
    private Vector3 initialRightPortraitPosition;

    private Queue<Dialogue.DialogueLine> sentences;

    private Dialogue.DialogueLine currentLine;
    private Dialogue.CharacterSide lastSpeakingSide;

    private bool isTyping;
    private bool isFading;

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
        secondaryBackgroundImage.gameObject.SetActive(false);

        skipButton.onClick.AddListener(ShowSkipConfirmation);
        confirmSkipButton.onClick.AddListener(SkipDialogue);
        cancelSkipButton.onClick.AddListener(HideSkipConfirmation);

        // Start playing the music
        PlayMusic();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            if (isFading)
            {
                return; // Prevent any interaction while fading is in progress
            }

            if (isTyping)
            {
                FinishTyping();
            }
            else if (fadeCoroutine == null)
            {
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

        // Handle fade-to-black transitions
        if (currentLine.fadeToBlackTransitionBefore)
        {
            fadeCoroutine = StartCoroutine(FadeToBlackTransition(() => DisplayLineWithTransitions()));
        }
        else if (currentLine.fadeToBlackTransitionAfter)
        {
            DisplayLineWithTransitions();
            fadeCoroutine = StartCoroutine(FadeToBlackTransition(null));
        }
        else
        {
            DisplayLineWithTransitions();
        }
    }

    private IEnumerator FadeToBlackTransition(System.Action callback)
    {
        isFading = true;

        fadePanel.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn(fadePanel, fadeDuration));

        // Execute the callback (e.g., update the dialogue) while the screen is black
        callback?.Invoke();

        yield return new WaitForSeconds(0.1f); // Pause briefly while the screen is black

        yield return StartCoroutine(FadeOut(fadePanel, fadeDuration));

        fadePanel.gameObject.SetActive(false);

        isFading = false;
        fadeCoroutine = null;
    }

    private void DisplayLineWithTransitions()
    {
        characterNameText.text = currentLine.characterName;

        if (currentLine.newBackground != null)
        {
            // Cross-fade to the new background if they are different
            if (backgroundImage.sprite != currentLine.newBackground)
            {
                if (backgroundFadeCoroutine != null)
                {
                    StopCoroutine(backgroundFadeCoroutine);
                }
                backgroundFadeCoroutine = StartCoroutine(CrossFadeBackground(currentLine.newBackground));
            }
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

    IEnumerator CrossFadeBackground(Sprite newBackground)
    {
        isFading = true;

        secondaryBackgroundImage.sprite = newBackground;
        secondaryBackgroundImage.color = new Color(1, 1, 1, 0);
        secondaryBackgroundImage.gameObject.SetActive(true);

        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            float alpha = elapsedTime / fadeDuration;

            // Keep original background fully visible and fade in new background on top
            secondaryBackgroundImage.color = new Color(1, 1, 1, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the new background fully visible and deactivate the secondary background
        backgroundImage.sprite = newBackground;
        backgroundImage.color = new Color(1, 1, 1, 1);
        secondaryBackgroundImage.gameObject.SetActive(false);

        isFading = false;
        backgroundFadeCoroutine = null;
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        int charIndex = 0;
        bool insideTag = false;

        while (charIndex < sentence.Length)
        {
            if (sentence[charIndex] == '<')
            {
                insideTag = true;
            }

            dialogueText.text += sentence[charIndex];
            charIndex++;

            if (sentence[charIndex - 1] == '>')
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

    // Music playback methods
    private void PlayMusic()
    {
        if (audioSource == null || introMusicClip == null || loopMusicClip == null)
        {
            Debug.LogWarning("Missing audio source or music clips. Please assign them in the Inspector.");
            return;
        }

        audioSource.clip = introMusicClip;
        audioSource.loop = false;
        audioSource.Play();
        Invoke(nameof(StartLoopingMusic), Mathf.Max(introMusicClip.length - 0.5f, 0));
    }

    private void StartLoopingMusic()
    {
        if (audioSource == null || loopMusicClip == null)
        {
            return;
        }

        audioSource.clip = loopMusicClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
