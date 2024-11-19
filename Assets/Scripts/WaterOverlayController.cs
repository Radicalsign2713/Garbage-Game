using System.Collections;
using UnityEngine;

public class WaterOverlayController : MonoBehaviour
{
    public PlayerMovementController movementController; // Reference to the PlayerMovementController script
    public float fadeInSpeed = 2f;  // Speed for fading in
    public float maxYOffset = 0.5f;  // Maximum upward movement offset when appearing

    private SpriteRenderer spriteRenderer;  // Reference to SpriteRenderer for controlling the sprite's visibility
    private Vector3 originalPosition;  // Store the original local position of the overlay
    private bool isActive = false;  // Check if the overlay is currently active

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (movementController == null)
        {
            Debug.LogError("PlayerMovementController reference is not assigned.");
            return;
        }

        // Make sure the sprite is initially invisible
        spriteRenderer.color = new Color(1, 1, 1, 0);
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (movementController.on_liquid && !isActive)
        {
            StartCoroutine(FadeInOverlay());
            isActive = true;
        }
        else if (!movementController.on_liquid && isActive)
        {
            StartCoroutine(FadeOutOverlay());
            isActive = false;
        }

        // Keep the overlay at the player's position
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = movementController.transform.position;
    }

    IEnumerator FadeInOverlay()
    {
        float elapsedTime = 0f;

        Vector3 startPosition = originalPosition - new Vector3(0, maxYOffset, 0);
        Vector3 targetPosition = originalPosition;

        // Gradually increase alpha and move the overlay upwards
        while (elapsedTime < fadeInSpeed)
        {
            elapsedTime += Time.deltaTime;

            // Adjust alpha to gradually fade in
            float newAlpha = Mathf.Clamp01(elapsedTime / fadeInSpeed);
            spriteRenderer.color = new Color(1, 1, 1, newAlpha);

            // Move the overlay upwards
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, newAlpha);

            yield return null;
        }

        spriteRenderer.color = new Color(1, 1, 1, 1);
        transform.localPosition = targetPosition;
    }

    IEnumerator FadeOutOverlay()
    {
        float elapsedTime = 0f;

        // Gradually decrease alpha to fade out
        while (elapsedTime < fadeInSpeed)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Clamp01(1 - (elapsedTime / fadeInSpeed));
            spriteRenderer.color = new Color(1, 1, 1, newAlpha);

            yield return null;
        }

        // Make sure the sprite is completely invisible
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }
}
