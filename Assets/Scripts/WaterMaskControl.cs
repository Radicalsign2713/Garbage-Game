using UnityEngine;

public class MCWaterMaskController : MonoBehaviour
{
    public PlayerMovementController movementController; // Reference to the PlayerMovementController script
    private Renderer maskRenderer;

    void Start()
    {
        if (movementController == null)
        {
            Debug.LogError("PlayerMovementController reference is not assigned.");
            return;
        }

        maskRenderer = GetComponent<Renderer>();
        if (maskRenderer == null)
        {
            Debug.LogError("Renderer component is not found on the GameObject.");
        }
    }

    void Update()
    {
        // If the player is solely on liquid, deactivate the mask
        if (movementController.on_liquid && !movementController.on_island)
        {
            SetMaskActive(false);
        }
        else
        {
            // In every other frame, activate the mask
            SetMaskActive(true);
        }
    }

    private void SetMaskActive(bool active)
    {
        if (maskRenderer != null)
        {
            maskRenderer.enabled = active;
        }
    }
}
