using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using System.Collections; // Import System.Collections for IEnumerator and coroutines

public class LifeUI : MonoBehaviour
{
    public TextMeshProUGUI textLife; // Reference to the TextMeshProUGUI component to display lives
    private int lives;  // Initial lives
    private bool isUpdatingLives = false; // Flag to prevent continuous updates
    private ScoreController scoreController;  // Reference ScoreController

    void Start()
    {
        lives = scoreController.lives;
        // Check if the Canvas or TextMeshProUGUI component is active before initializing
        if (textLife != null && textLife.gameObject.activeInHierarchy)
        {
            // Initialize the text with the starting number of lives
            UpdateLivesText();
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI component is not active in the hierarchy.");
        }
    }

    // Function to modify the lives, can be positive (to increase) or negative (to decrease)
    public void ChangedLife(int change)
    {
        // Check if the text component is active before updating
        if (textLife != null && textLife.gameObject.activeInHierarchy)
        {
            if (isUpdatingLives) return; // Prevent multiple updates if already updating

            isUpdatingLives = true; // Set the flag to true to block further updates

            lives -= change;  // Modify the number of lives based on the 'change' parameter

            // Ensure that the number of lives doesn't go below 0
            //lives = Mathf.Max(lives, 0);

            // Update the text component with the new number of lives
            UpdateLivesText();

            // After updating the lives, reset the flag after a small delay (for example, 0.1 seconds)
            StartCoroutine(ResetUpdateFlag());
        }
    }

    // Function to update the text in the UI
    private void UpdateLivesText()
    {
        textLife.text = string.Format("{0}", lives);  // Display the updated lives count
    }

    // Public Coroutine to reset the flag after a short delay
    public IEnumerator ResetUpdateFlag()
    {
        yield return new WaitForSeconds(0.1f); // Wait for a short period
        isUpdatingLives = false; // Allow further updates
    }
}
