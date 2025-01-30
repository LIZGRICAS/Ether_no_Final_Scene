using UnityEngine;

namespace Enemies.DoubleDragon
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float chaseRadius = 10f;
        [SerializeField] private float attackRadius = 2f;
        // private Transform _player;

        [Header("Audio Sources")]
        [SerializeField] private AudioClip punch, shild, cross; // Clips de audio

        // Referencia al Animator para manejar las animaciones del Double_Dragon
        private Animator animator;
        private CameraShake cameraShake;

        // Enum de los estados del enemigo
        private enum State { Idle, Attack, Die }
        private State _currentState;

        // Variables de control para ataques
        private float timeBetweenAttacks = 3f;
        private float timeNextAttack;

        private void Start()
        {
            // _player = GameObject.FindGameObjectWithTag("Player").transform;
            _currentState = State.Idle;
            timeNextAttack = timeBetweenAttacks; // Inicializamos el tiempo para el siguiente ataque
            animator = GetComponent<Animator>();
            cameraShake = Camera.main.GetComponent<CameraShake>(); // Asume que la cámara principal tiene el script CameraShake
        }

        private void Update()
        {
            switch (_currentState)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Attack:
                    Attack();
                    break;
                case State.Die:
                    Die();
                    break;
            }

            // Verificamos las transiciones de estado
            // CheckStateTransitions();
        }

        // private void CheckStateTransitions()
        // {
        //     float distanceToPlayer = Vector3.Distance(_player.position, transform.position);

        //     if (_currentState == State.Idle)
        //     {
        //         if (distanceToPlayer <= chaseRadius)
        //         {
        //             _currentState = State.Attack;
        //         }
        //     }

        //     if (_currentState == State.Attack)
        //     {
        //         if (distanceToPlayer > attackRadius)
        //         {
        //             _currentState = State.Idle;
        //         }
        //     }
        // }

        #region States

        private void Idle()
        {
            // El enemigo está en reposo, animacion de "quieto" 
            animator.SetTrigger("Idle"); 
        }

        private void Attack()
        {
            // Reducir el tiempo de espera entre ataques
            timeNextAttack -= Time.deltaTime;

            if (timeNextAttack <= 0f)
            {
                // Elegir aleatoriamente el tipo de ataque
                ExecuteAttack();
                timeNextAttack = timeBetweenAttacks; // Resetear el temporizador de ataque
            }
        }

        private void Die()
        {
            // logica de animación de muerte.
            animator.SetTrigger("Die"); 
        }

        #endregion

        #region Attack Logic

        private void ExecuteAttack()
        {
            // Elegir aleatoriamente qué tipo de ataque ejecutar para cada cabeza
            int attackHead1 = Random.Range(0, 2); // 0 para fuego, 1 para lluvia de fuego
            int attackHead2 = Random.Range(0, 2); // 0 para morder, 1 para terremoto

            // Llamar al Trigger correspondiente de las animaciones de la cabeza 1
            if (attackHead1 == 0)
            {
                animator.SetTrigger("Throw_fire");  // Activamos la animación de la cabeza 1 tirando fuego
            }
            else
            {
                animator.SetTrigger("Cast_rain_of_fire");  // Activamos la animación de lluvia de fuego
            }

            // Llamar al Trigger correspondiente de las animaciones de la cabeza 2
            if (attackHead2 == 0)
            {
                animator.SetTrigger("Bite");  // Activamos la animación de morder (cabeza 2)
            }
            else
            {
                animator.SetTrigger("Earthquake");  // Activamos la animación de terremoto (cabeza 2)
            }
        }

        #endregion
        
        // Este es el evento de animación que se llama durante la animación
        // Asegúrate de que este método sea el que se llame en el Animation Event
        public void OnAttackShake()
        {
            cameraShake.StartShake(0.1f, 0.5f);  // Magnitud y duración del temblor
        }
    }
}