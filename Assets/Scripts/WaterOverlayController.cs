using System.Collections;
using UnityEngine;

public class WaterOverlayController : MonoBehaviour
{
    public PlayerMovementController movementController; // Reference to the PlayerMovementController script
    public float fadeSpeed = 0.1f;  // Speed for both fading in and fading out

    private SpriteRenderer spriteRenderer;  // Reference to SpriteRenderer for controlling the sprite's visibility
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
    }

    void Update()
    {
        if (movementController.on_liquid && !movementController.on_island && !isActive)
        {
            StartCoroutine(FadeInOverlay());
            isActive = true;
        }
        else if ((!movementController.on_liquid || movementController.on_island) && isActive)
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

        // Gradually increase alpha to fade in quickly
        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;

            // Adjust alpha to gradually fade in
            float newAlpha = Mathf.Clamp01(elapsedTime / fadeSpeed);
            spriteRenderer.color = new Color(1, 1, 1, newAlpha);

            yield return null;
        }

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    IEnumerator FadeOutOverlay()
    {
        float elapsedTime = 0f;

        // Gradually decrease alpha to fade out quickly
        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Clamp01(1 - (elapsedTime / fadeSpeed));
            spriteRenderer.color = new Color(1, 1, 1, newAlpha);

            yield return null;
        }

        // Make sure the sprite is completely invisible
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Ensure the overlay fades out when touching anything tagged as "Island"
        if (other.CompareTag("Island") && isActive)
        {
            StartCoroutine(FadeOutOverlay());
            isActive = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Reset the overlay status when leaving the island
        if (other.CompareTag("Island"))
        {
            isActive = false;
        }
    }
}
