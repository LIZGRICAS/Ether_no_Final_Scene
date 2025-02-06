using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena o salir

public class ScoreController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip death, getHit; // Audio clips for player death and damage

    [Header("Player Stats")]
    [SerializeField] private int maxLives = 5; // Maximum player lives
    [SerializeField] private int maxHealth = 100; // Maximum player health

    [SerializeField] public Animator playerAnimator; // Reference to player Animator
    private HealthBar healthBar; // Reference to health bar
    private LifeUI lifeUI; // Reference to life UI (assuming you have this script for life updates)

    public int lives; // Number of player lives (renamed from 'live' to 'lives')
    public float health; // Current player health
    public float immunityTime = 1f; // Immunity time after taking damage
    private bool isImmune; // Indicates if the player is immune to damage
    public float knockBackForceX; // Knockback force in X
    public float knockBackForceY; // Knockback force in Y
    private bool isDead = false;  // Flag to prevent multiple death updates

    [Header("Player Stats Image Bar")]
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    private PlayerMovement playerMovement;

    [Header("Game Over Canvas")]
    public GameObject gameOverCanvas;  // Reference to the "Game Over" Canvas in the scene

    void Start()
    {
        // Initialize components
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        // Set initial values
        lives = maxLives;
        health = maxHealth;

        // Initialize health and life bars (if they exist in the scene)
        healthBar = FindObjectOfType<HealthBar>(); // Assuming HealthBar is a script in the scene
        lifeUI = FindObjectOfType<LifeUI>(); // Assuming LifeUI is a script in the scene

        // Hide the game over canvas initially
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the player has died
        if (health <= 0 && !isDead)
        {
            isDead = true;  // Set the death flag to true to prevent further updates

            AudioManager.Instance.PlaySound(death); // Play death sound
            playerAnimator.SetTrigger("Death"); // Trigger death animation

            if (lifeUI != null) 
            {
                // Update lives UI only once
                lifeUI.ChangedLife(1); 
            }

            // Activate the game over canvas after a short delay to give time for updates
            Invoke("ActivateGameOverCanvas", 1f); // Delay the activation to give time for the UI to update
        }

        if (isDead)
        {
            playerAnimator.SetTrigger("Death"); // Trigger death animation
        }

        // Ensures that health and lives do not exceed the maximum values
        health = Mathf.Clamp(health, 0, maxHealth);
        lives = Mathf.Clamp(lives, 0, maxLives);

        // Update health and life bars if they exist
        if (healthBar != null) healthBar.InitializeLifeBar(health);  // Update health bar
        Debug.Log("Value of health: " + health + ", Value of lives: " + lives);  // Debug info
    }

    // Method to activate the Game Over canvas
    private void ActivateGameOverCanvas()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);  // Activate the Game Over canvas
        }

        // You can also disable player controls, game objects, etc., here if necessary
    }

    // Method to restart the game (reload the current scene)
    public void RestartGame()
    {
        // Reload the current scene (restarts the game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to exit the game
    public void ExitGame()
    {
        // Close the application (works in a build, not in the editor)
        Application.Quit();
        Debug.Log("Game is exiting...");
    }

    // Activate temporary immunity
    public IEnumerator Immunity()
    {
        isImmune = true;
        sprite.color = new Color(1f, 1f, 1f, 0.5f); // Visual change to indicate immunity
        yield return new WaitForSeconds(immunityTime);
        sprite.color = new Color(1f, 1f, 1f, 1f); // Back to normal
        isImmune = false;
    }

    // Increase player's lives
    public void Heal(int amount)
    {
        lives = Mathf.Min(lives + amount, maxLives); // Increase lives, but limit to maxLives
    }

    // Decrease player's health
    public void UseHealth(int amount)
    {
        health = Mathf.Max(0, health - amount); // Decrease health, but prevent going below 0
    }
}
