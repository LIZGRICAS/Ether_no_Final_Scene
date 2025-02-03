using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip death, getHit; // Clips de audio para la muerte y daño del jugador

    [Header("Player Stats")]
    [SerializeField] private int maxLives = 5; // Vida máxima del jugador
    [SerializeField] private int maxHealth = 100; // Salud máxima del jugador

    public int live; // Número de vidas del jugador
    public float health; // Salud actual del jugador
    public float inmunityTime = 1f; // Tiempo de inmunidad después de recibir daño
    private bool isInmune; // Indica si el jugador es inmune al daño
    public float knockBackForceX; // Fuerza de retroceso en X
    public float knockBackForceY; // Fuerza de retroceso en Y

    [Header("Player Stats Image Bar")]
    public Image liveImg; // Imagen de barra de vida
    public Image healthImg; // Imagen de barra de salud
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    
    // Referencia al Animator del jugador, asignado manualmente desde el inspector
    public Animator playerAnimator;
    [SerializeField] private Animator enemyAnimator;
    
    
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        live = maxLives;
        health = maxHealth;
    }

    void Update()
    {
        // Actualiza las barras de vida y salud
        liveImg.fillAmount = (float)live / maxLives;
        healthImg.fillAmount = health / maxHealth;

        // Revisa si el jugador ha muerto
        if (live <= 0)
        {
            AudioManager.Instance.PlaySound(death);
            SceneManager.LoadScene("GameOver");
        }

        // Asegura que la salud y la vida no excedan los valores máximos
        health = Mathf.Clamp(health, 0, maxHealth);
        live = Mathf.Clamp(live, 0, maxLives);
    }

   
    // Aplica daño al jugador
    public void TakeDamage(int damage, Collider2D enemy)
    {
        enemyAnimator.SetTrigger("skill_1");
        AudioManager.Instance.PlaySound(getHit);
        playerAnimator.SetTrigger("Hurt");
        AudioManager.Instance.PlaySound(getHit);

        health -= damage;

        if (health <= 0)
        {
            // Si la salud llega a cero, se pierde una vida y se reinicia la salud
            live--;
            health = maxHealth;
        }

        // Retroceso
        if (enemy.transform.position.x > transform.position.x)
        {
            rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }

        // Activar inmunidad temporal
        StartCoroutine(Inmunity());
    }

    // Activar inmunidad temporal
    private IEnumerator Inmunity()
    {
        isInmune = true;
        sprite.color = new Color(1f, 1f, 1f, 0.5f); // Cambio visual para indicar inmunidad (puedes cambiar esto)
        yield return new WaitForSeconds(inmunityTime);
        sprite.color = new Color(1f, 1f, 1f, 1f); // Vuelve a la normalidad
        isInmune = false;
    }

    // Incrementa la vida del jugador
    private void Heal(int amount)
    {
        live = Mathf.Min(live + amount, maxLives);
    }

    // Reduce la salud del jugador
    public void UseHealth(int amount)
    {
        health = Mathf.Max(0, health - amount);
    }
}
