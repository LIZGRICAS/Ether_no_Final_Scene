using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [Header("Puntos patrullar")] 
    [SerializeField] private Transform[] points;


    
    [Header("Configuracion de la IA")]
    public float speed;
    private Rigidbody2D rb;
    private Transform puntoActual;
    private int indicePunto = 0;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        if (points.Length <= 0)
        {
            print("hola que ase");
            return;
        }
        puntoActual = points[indicePunto];
    }

    // FixedUpdate para manejo de fisica y evitar la interferencia del time y deltatime
    void FixedUpdate()
    {
        float ejeY = rb.linearVelocity.y;
        // se normaliza ya que el vector de trayecto es muy grande
            Vector2 direccionPunto = (puntoActual.position - transform.position).normalized;
            
            // movimiento del enemigo constante sin importar la lejania del punto de patrullaje
            if (direccionPunto.x != 0)
            {
                if (direccionPunto.x > 0)
                {
                    direccionPunto.x = 1;
                }
                else
                {
                    direccionPunto.x = -1;
                }
            }
        
            Vector2 direccionVelocidad = direccionPunto * speed;
            rb.linearVelocity = direccionVelocidad;
        
    }
    
    //update para verificar cuando queremos cambiar el punto
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
