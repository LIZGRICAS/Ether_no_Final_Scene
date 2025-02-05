using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip death, getHit; // Audio clips for player death and damage

    [Header("Player Stats")]
    [SerializeField] private int maxLives = 5; // Maximum player health
    [SerializeField] private int maxHealth = 100; // Maximum player health

    [SerializeField] public Animator enemyAnimator;  // Referencia al Animator del enemigo
    [SerializeField] public Animator playerAnimator; // Referencia al Animator del jugador
    public int live; // Number of player lives
    public float health; // Current player health
    public int liveEnemy = 3; // Enemy health
    public float immunityTime = 1f; // Immunity time after taking damage
    private bool isImmune; // Indicates if the player is immune to damage
    public float knockBackForceX; // Knockback force in X
    public float knockBackForceY; // Knockback force in Y

    [Header("Player Stats Image Bar")]
    public Image liveImg; // Life bar image
    public Image healthImg; // Health bar image
    private SpriteRenderer sprite;
    private Rigidbody2D rb;


    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        enemyAnimator = enemyAnimator.GetComponent<Animator>();
        live = maxLives;
        health = maxHealth;

    }

    void Update()
    {
        // Updates the health and life bars
        liveImg.fillAmount = (float)live / maxLives;
        healthImg.fillAmount = health / maxHealth;

        print("Value of health: " + health + ", Value of liveEnemy: " + liveEnemy);

        // Checks if the player has died
        if (health <= 0)
        {
            AudioManager.Instance.PlaySound(death);
            playerAnimator.SetTrigger("Death");
            SceneManager.LoadScene("GameOver");
        }

        // Si la salud del enemigo llega a cero o menos, activamos la animación de muerte
        if (liveEnemy <= 0)
        {
            print("El enemigo ha muerto");
            enemyAnimator.SetTrigger("death"); 
            AudioManager.Instance.PlaySound(death);
        }

        // Ensures that health and life do not exceed the maximum values
        health = Mathf.Clamp(health, 0, maxHealth);
        live = Mathf.Clamp(live, 0, maxLives);
    }


    // Apply damage to the enemy and return the remaining health
    public int TakeDamage(int damage)
    {
        // Reduce la salud del enemigo
        liveEnemy -= damage;

        // Retorna la salud restante del enemigo
        return liveEnemy;
    }


    // Activate temporary immunity
    public IEnumerator Immunity()
    {
        isImmune = true;
        sprite.color = new Color(1f, 1f, 1f, 0.5f); // Visual change to indicate immunity (you can change this)
        yield return new WaitForSeconds(immunityTime);
        sprite.color = new Color(1f, 1f, 1f, 1f); // Back to normal
        isImmune = false;
    }

    // Increase player's health
    public void Heal(int amount)
    {
        live = Mathf.Min(live + amount, maxLives);
    }

    // Decrease player's health
    public void UseHealth(int amount)
    {
        health = Mathf.Max(0, health - amount);
    }
}
