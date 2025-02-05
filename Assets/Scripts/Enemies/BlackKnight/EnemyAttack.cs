using UnityEngine;
using System.Collections; // Necesario para las Coroutines

public class EnemyAttack : MonoBehaviour
{
    [Header("Configuración del Enemigo")]
    [SerializeField] public Animator enemyAnimator;  // Referencia al Animator del enemigo
    [SerializeField] public Animator playerAnimator; // Referencia al Animator del jugador
    public string attackAnimationTrigger = "skill_1";  // Nombre del Trigger de ataque en el enemigo
    public string hurtAnimationTrigger = "Hurt";     // Nombre del Trigger de daño en el jugador
    private Rigidbody2D rb;
    public AudioClip getHit; // Referencia al sonido de golpe

    public float knockBackForceX = 10f; // Fuerza de retroceso en X
    public float knockBackForceY = 5f;  // Fuerza de retroceso en Y

    private bool isPlayerInRange = false; // Para saber si el jugador está dentro del área de colisión

    private int atkTimes = 0;

    // Reference ScoreController
    private ScoreController scoreController;

    void Awake()
    {
        // Inicializa los componentes
        rb = enemyAnimator.GetComponent<Rigidbody2D>(); // Asignar el Rigidbody2D del enemigo
        scoreController = FindObjectOfType<ScoreController>(); // Reference  ScoreController
    }

    void Start(){

        // Verifica si el scoreController fue encontrado
        if (scoreController == null)
        {
            Debug.LogError("ScoreController no encontrado en la escena.");
        }
    }
    
    private void Update()
    {
        // Si el jugador ya no está en rango y la animación sigue activa y si ya se ha realizado ataque previo
        // forzar al enemigo a que se detenga de alguna forma.
        if (!isPlayerInRange && atkTimes > 0)
        {
            // Comprobamos si no hay colisión con el jugador y si el enemigo tiene animaciones activas
            if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("death")) 
            {
                // comportamiento alternativo cuando el jugador ya no está en rango
                // Poner una animación de "idle" 
                enemyAnimator.SetTrigger("idle_1"); // Activamos animación de "idle" si el jugador salió del rango
                enemyAnimator.SetTrigger("idle_2"); // Activamos animación de "idle" si el jugador salió del rango
            }
        }
    }

    // Se llama cuando el jugador entra en el área de colisión
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //accedemos a las propiedades de la clase ScoreController
        ScoreController playerScoreController = collision.GetComponent<ScoreController>();
        // Verificamos si la colisión es con el jugador
        if (collision.CompareTag("Player"))
        {
            // Rotamos el enemigo para que mire hacia el jugador
            RotateTowardsPlayer(collision);
            enemyAnimator.SetTrigger(attackAnimationTrigger); // Activamos la animación de ataque en el enemigo
            // activamos la coroutine para retrasar la animación de daño en el jugador 
            StartCoroutine(PlayHurtAnimationWithDelay(1f));
            // Retroceso
            KnockBack(collision);
            StartCoroutine(scoreController.Immunity());
            // Marcamos que el jugador está en rango
            isPlayerInRange = true;
            atkTimes++;
            playerScoreController.UseHealth(20); // Reduce la salud del jugador
        }
    }

    // Se llama cuando el jugador sale del área de colisión
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verificamos si el jugador salió del área de colisión
        if (collision.CompareTag("Player"))
        {
            // Si el jugador sale, detenemos las animaciones de ataque
            enemyAnimator.ResetTrigger(attackAnimationTrigger); // Detenemos la animación de ataque
            isPlayerInRange = false; // El jugador ya no está en rango
        }
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
    private void RotateTowardsPlayer(Collider2D playerCollider)
    {
        Vector2 directionToPlayer = playerCollider.transform.position - transform.position;

        // Si el jugador está detrás del enemigo en el eje X, invertir la rotación del enemigo
        if (directionToPlayer.x < 0) // Si el jugador está a la izquierda del enemigo
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Giramos el enemigo hacia la izquierda
        }
        else // Si el jugador está a la derecha del enemigo
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Giramos el enemigo hacia la derecha

        }

    }

    // Coroutine para esperar un segundo antes de activar la animación de daño en el jugador
    IEnumerator PlayHurtAnimationWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Esperar el tiempo especificado (1 segundo)
        playerAnimator.SetTrigger(hurtAnimationTrigger);  // Activamos la animación de daño en el jugador
        AudioManager.Instance.PlaySound(getHit); // Reproducir sonido de golpe
    }
}
