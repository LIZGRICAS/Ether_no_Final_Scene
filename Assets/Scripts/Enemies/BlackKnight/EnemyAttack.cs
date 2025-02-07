using System;
using UnityEngine;
using System.Collections; // Necesario para las Coroutines

public class EnemyAttack : MonoBehaviour
{
    [Header("Configuration to Player")]
    [SerializeField] public Animator playerAnimator; // Referencia al Animator del jugador
    [SerializeField] public Transform player;
    private bool isPlayerInRange = false; // Para saber si el jugador está dentro del área de colisión

    [Header("Configuration to Enemy")] 
    public Animator enemyAnimator; // Referencia al Animator del enemigo
    public Rigidbody2D rb;
    [SerializeField] public AudioClip getHit; // Referencia al sonido de golpe
    
    [SerializeField] private GameObject swordInstance; 

    
    [Header("Animations")]
    public string attackAnimationTrigger = "skill_1"; // Nombre del Trigger de ataque en el enemigo
    public string deathAnimationTrigger = "death"; // Nombre del Trigger de muerte del enemigo
    
    [Header("Lives")]
    public int livesEnemy = 3;
    private ScoreController scoreController; // Reference ScoreController

    [Header("Attack")] 
    [SerializeField] private Transform attackController;

    [SerializeField] private float radioAttack;
    [SerializeField] private float damageAttack;
    
    public float knockBackForceX = 10f; // Fuerza de retroceso en X
    public float knockBackForceY = 5f; // Fuerza de retroceso en Y
    
    private int atkTimes = 0;
    private bool lookRight = false;

    
    void Awake()
    {
        // Inicializa los componentes
        rb = enemyAnimator.GetComponent<Rigidbody2D>(); // Asignar el Rigidbody2D del enemigo
        enemyAnimator = GetComponent<Animator>();
        swordInstance = GameObject.FindGameObjectWithTag("Sword");
        swordInstance.SetActive(false);
    }

    void Start()
    {
        
        // Verifica si el scoreController fue encontrado
        if (scoreController == null)
        {
            Debug.LogError("ScoreController no encontrado en la escena.");
        }
        
        // Asegúrate de que el prefab esté asignado en el Inspector
        if (swordInstance == null)
        {
            Debug.LogError("No se ha asignado el prefab de la espada.");
        }
    }

    private void Update()
    {
        float distancePlayer = Vector2.Distance(transform.position, player.position);
        enemyAnimator.SetFloat("distancePlayer", distancePlayer);
    }

    // Método para aplicar el retroceso
    private void KnockBack(Collider2D collision)
    {
        // Calculamos la dirección de retroceso en función de la posición del jugador
        if (enemyAnimator.transform.position.x > collision.transform.position.x)
        {
            // Si el enemigo está a la derecha del jugador, empujamos al jugador hacia la izquierda
            rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Impulse);
        }
        else
        {
            // Si el enemigo está a la izquierda del jugador, empujamos al jugador hacia la derecha
            rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Impulse);
        }
    }


    // Método para rotar al enemigo hacia el jugador si el jugador está detrás del enemigo
    public void RotateTowardsPlayer()
    {
     // Si el jugador está detrás del enemigo en el eje X, invertir la rotación del enemigo
        if ((player.position.x > transform.position.x && !lookRight) || (player.position.x < transform.position.x && lookRight)) // Si el jugador está a la izquierda del enemigo
        {
            lookRight = !lookRight; // Giramos el enemigo 
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }

    }

    public void TakeDamage(int damage)
    {
        livesEnemy -= damage;

        if (livesEnemy <= 0)
        {
            enemyAnimator.SetTrigger(deathAnimationTrigger);
            Death();
        }
    }
    
    public void Death()
    {
        // Destroy(gameObject);
      
        // Inicia la coroutine para esperar y destruir el enemigo, pero no la espada
        StartCoroutine(WaitAndDestroyEnemy());
        
    }
    
    private IEnumerator WaitAndDestroyEnemy()
    {
        // Espera un tiempo (ajustalo según la duración de la animación de muerte del enemigo)
        yield return new WaitForSeconds(1f);  // Supón que la animación de muerte dura 1 segundo
        // se destruye el enemigo
        Destroy(gameObject);
        
        // Ajustar la posición de la espada solo en el eje X, Y y Z
        // Puedes mover la espada hacia adelante o ligeramente hacia algún lado en el eje X:
        float offsetX = 0.9f; // Este es un valor positivo si el enemigo está mirando a la derecha
        float offsetY = 5f; // Este es el desplazamiento hacia arriba en el eje Y
        

        swordInstance.transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY , transform.position.z);

        // Aseguramos que la espada está activada
        swordInstance.SetActive(true);
    }
    
    public void Attack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(attackController.position, radioAttack);

        foreach (Collider2D colision in objects)
        {
            if (colision.gameObject.tag == "Player")
            {
                colision.GetComponent<PlayerAttack>().TakeDamage(damageAttack);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackController.position, radioAttack);
    }
}
