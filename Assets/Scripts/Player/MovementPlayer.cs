using UnityEngine;

namespace Script
{
    public class MovementPlayer : MonoBehaviour
    {
        private float moveVertical;
        private float moveHorizontal;
        
        public float moveSpeed = 3f; // Velocidad de movimiento
        private Rigidbody rb; // Referencia al Rigidbody

        private void Awake()
        {
            rb = GetComponent<Rigidbody>(); // Obtenemos la referencia al Rigidbody
        }

        private void Update()
        {
            moveVertical = Input.GetAxis("Vertical"); // W/S o joystick arriba/abajo
            moveHorizontal = Input.GetAxis("Horizontal"); // A/D o joystick izquierda/derecha

            // Movimiento con física usando Rigidbody
            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * moveSpeed;
            rb.MovePosition(transform.position + movement * Time.deltaTime);
        }
    }
}