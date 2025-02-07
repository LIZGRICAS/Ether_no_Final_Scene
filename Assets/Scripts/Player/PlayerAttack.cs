using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    ScoreController scoreController;
    public Animator playerAnimator;
    public string hurtAnimationTrigger = "Hurt"; // Nombre del Trigger de daño en el jugador
    public string deathAnimationTrigger = "Death"; // Nombre del Trigger de daño en el jugador
    [SerializeField] public AudioClip getHit; // Referencia al sonido de golpe
    [SerializeField] private Transform hitController;
    [SerializeField] private float hitRadio;
    [SerializeField] private int hitDamage;
    

    void Start()
    {
        // Initialize components
        scoreController = GetComponent<ScoreController>();
        playerAnimator = GetComponent<Animator>();
    }
    

    public void TakeDamage(float damage)
    {
        scoreController.health -= damage;

        playerAnimator.SetTrigger(hurtAnimationTrigger); // Activamos la animación de daño en el jugador
        AudioManager.Instance.PlaySound(getHit); // Reproducir sonido de golpe
        
        if (scoreController.health <= 0)
        {
            playerAnimator.SetTrigger(deathAnimationTrigger);
            
        }
    }

    public void Hit()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(hitController.position, hitRadio);

        foreach (Collider2D colision in objects)
        {
            if (colision.CompareTag("Enemy"))
            {
                colision.GetComponent<EnemyAttack>().TakeDamage(hitDamage);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitController.position, hitRadio);
    }
}
