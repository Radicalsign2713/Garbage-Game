using UnityEngine;
using UnityEngine.UI;

public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 50f; // Adjust this to set the speed of the scrolling
    public RectTransform creditsText; // Assign this to your Credits UI text or panel

    private float startingPositionY;
    private float screenHeight;

    void Start()
    {
        // Save the initial position of the text
        startingPositionY = creditsText.anchoredPosition.y;
        // Get the height of the screen for looping purposes
        screenHeight = Screen.height;
    }

    void Update()
    {
        // Scroll the text upwards
        if (creditsText != null) // Ensure creditsText is assigned
        {
            creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // Check if the text is completely off the top of the screen
            if (creditsText.anchoredPosition.y > screenHeight + creditsText.rect.height)
            {
                // Reset position to start from the bottom again if you want to loop it
                creditsText.anchoredPosition = new Vector2(creditsText.anchoredPosition.x, startingPositionY);
            }
        }
    }
}
