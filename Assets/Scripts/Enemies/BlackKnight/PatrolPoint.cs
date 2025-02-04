using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [Header("Puntos patrullar")] 
    [SerializeField] private Transform[] points;


    
    [Header("Configuracion de la IA")]
    public float speed;
    public float detectionRange = 5f;  // Rango en el que el enemigo detecta al jugador
    private Rigidbody2D rb;
    private Transform puntoActual;
    private int indicePunto = 0;
    private Transform player;  // Referencia al jugador
    private bool isPlayerInRange = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
    // Si no hay puntos de patrullaje, no podemos patrullar
        if (points.Length <= 0)
        {
            return;
        }
 // Asignamos el primer punto de patrullaje
        puntoActual = points[indicePunto];

        // Buscamos al jugador en la escena
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // FixedUpdate para manejo de fisica y evitar la interferencia del time y deltatime
    void FixedUpdate()
    {
        if (player == null) return;  // Asegurarse de que haya un jugador en la escena

        // Detectar la distancia entre el enemigo y el jugador
        float playerDistance = Vector2.Distance(transform.position, player.position);

        // Si el jugador está dentro del rango de detección, lo persigue
        if (playerDistance <= detectionRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }

        // Si el jugador está en rango, persigue al jugador
        if (isPlayerInRange)
        {
            // Dirección hacia el jugador
            Vector2 directionToPlayer = (player.position - transform.position).normalized;

            // Establecer velocidad hacia el jugador
            rb.linearVelocity = directionToPlayer * speed;
        }
        else
        {
            // Si no está en rango, patrullar
            float ejeY = rb.linearVelocity.y;
        // se normaliza ya que el vector de trayecto es muy grande
            Vector2 direccionPunto = (puntoActual.position - transform.position).normalized;
            
            // movimiento del enemigo constante sin importar la lejania del punto de patrullaje
            if (direccionPunto.x != 0)
            {
                if (direccionPunto.x > 0)
                {
                    direccionPunto.x = 1;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    direccionPunto.x = -1;
                    // Restauramos la rotación original (sin rotación en Y) si se mueve hacia la derecha
            transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        
            Vector2 direccionVelocidad = direccionPunto * speed;
            rb.linearVelocity = direccionVelocidad;
        }
        
    }
    
    // Cambio de punto de patrullaje cuando el enemigo llega al punto
    private void Update()
    {
        Vector2 puntoIr = new Vector2(puntoActual.position.x, 0);
        Vector2 dondeEstamos = new Vector2( transform.position.x, 0);
        // si la distancia del punto donde se encuentra el enemigo es mayor a 0.5 al punto de patrullaje, se activa la trayectoria al otro punto del enemigo
        if (Vector2.Distance(puntoIr, dondeEstamos) < 0.5f)
        {
            
            puntoActual= points[(indicePunto++)%points.Length];
        }
            
    }
}
