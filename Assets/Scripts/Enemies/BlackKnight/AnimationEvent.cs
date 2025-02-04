using UnityEngine;

namespace Enemies.BlackKnight
{
    public class AnimationEvent : MonoBehaviour
    {
        public GameObject enemy;
        private int atkTimes = 0;
        private Rigidbody2D rb;
        private Animator enemyAnimator;
        public float knockBackForceX; // Fuerza de retroceso en X
        public float knockBackForceY; // Fuerza de retroceso en Y

        public AudioClip getHit; // Referencia al sonido de golpe

        private ScoreController scoreController;

        void Awake()
        {
            // Inicializa los componentes
            rb = enemy.GetComponent<Rigidbody2D>(); // Asignar el Rigidbody2D del enemigo
            enemyAnimator = enemy.GetComponent<Animator>(); // Asignar el Animator del enemigo
        }

        public void AttackStart(Animator playerAnimator)
        {
            enemyAnimator.SetTrigger("skill_1"); // Primer ataque
            AudioManager.Instance.PlaySound(getHit); // Reproducir sonido de golpe
            playerAnimator.SetTrigger("Hurt"); // Activar animación de daño para el jugador
            enemyAnimator.SetTrigger("");
            // Activar inmunidad temporal
            StartCoroutine(scoreController.Inmunity());
        }

        // Método para aplicar efectos visuales
        public void AttackStartEffectObject()
        {
            Debug.Log("Fire Effect Object");
        }

        // Detectar colisión con el jugador y hacer daño
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Si el enemigo colisiona con el jugador
            if (collision.CompareTag("Player"))
            {
                Animator playerAnimator = collision.GetComponent<Animator>();
                ScoreController playerScoreController = collision.GetComponent<ScoreController>();

                atkTimes++;

                if (enemy && atkTimes <= 3 && playerScoreController != null)
                {
                    // Seleccionar el ataque dependiendo de los golpes
                    if (atkTimes == 1)
                    {
                        //animacion de ataques
                        AttackStart(playerAnimator);
                        // Retroceso
                        KnockBack(collision);

                    }
                    else if (atkTimes == 2)
                    {
                        //animacion de ataques
                        AttackStart(playerAnimator);
                        // Retroceso
                        KnockBack(collision);
                    }
                    else if (atkTimes == 3)
                    {
                        //animacion de ataques
                        AttackStart(playerAnimator);
                        // Retroceso
                        KnockBack(collision);

                        // Activar el daño en la salud total
                        playerScoreController.TakeDamage(10);
                    }

                    // Aplicar daño al jugador
                    if (playerScoreController != null)
                    {
                        playerScoreController.UseHealth(10); // Reduce la salud del jugador
                    }
                }
            }
        }

        // Método para aplicar el retroceso
        private void KnockBack(Collider2D collision)
        {
            if (enemy.transform.position.x > collision.transform.position.x)
            {
                rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Impulse);
            }
        }
    }
}
