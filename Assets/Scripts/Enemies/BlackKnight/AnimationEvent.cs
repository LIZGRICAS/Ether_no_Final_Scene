using UnityEngine;

namespace Enemies.BlackKnight
{
    public class AnimationEvent : MonoBehaviour {

        public GameObject enemy;
        private int atkTimes = 0;

        // Método para manejar la animación de ataque
        public void AttackStart()
        {
            Debug.Log("Enemy Attack Start");

            atkTimes++;
            if (enemy && atkTimes <= 3)
            {
                Animator enemyAnimator = enemy.GetComponent<Animator>();
                if (atkTimes == 1)
                {
                    enemyAnimator.SetTrigger("hit_1"); // Primer ataque
                }
                else if (atkTimes == 2)
                {
                    enemyAnimator.SetTrigger("hit_2"); // Segundo ataque
                }
                else if (atkTimes == 3)
                {
                    enemyAnimator.SetTrigger("hit_2");
                    enemyAnimator.SetTrigger("death"); // Matar al enemigo después del tercer golpe (o hacer otra animación)
                }
            }
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
                // Activamos la animación del jugador para el daño recibido
                Animator playerAnimator = collision.GetComponent<Animator>();
                if (playerAnimator != null)
                {
                    playerAnimator.SetTrigger("Hurt"); // Activar animación de daño en el jugador
                }

                // Aplicar daño al jugador
                ScoreController playerScoreController = collision.GetComponent<ScoreController>();
                if (playerScoreController != null)
                {
                    playerScoreController.UseHealth(10); // Reduce la salud del jugador
                }
            }
        }
    }
}
