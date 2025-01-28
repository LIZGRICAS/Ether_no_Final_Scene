using UnityEngine;

namespace SmallScaleInteractive._2DCharacter.Objects
{
    public class BoatAnimationController : MonoBehaviour
    {
        private Animator animator;
        private Transform cameraTransform;

        public float targetX = 13.1f; // La posición X exacta que queremos comprobar

        void Start()
        {
            animator = GetComponent<Animator>(); // Obtener el Animator del objeto Boat
            cameraTransform = Camera.main.transform; // Obtener la referencia a la cámara principal
        }

        void Update()
        {
            // Verificar si la posición X de la cámara es exactamente 13.1
            if (Mathf.Approximately(cameraTransform.position.x, targetX))
            {
                // Activar la animación si la cámara está en la misma posición X
                animator.SetBool("IsBoatInPosition", true); // Asegúrate de tener este parámetro en el Animator
            }
            else
            {
                // Desactivar la animación si la cámara se aleja
                animator.SetBool("IsBoatInPosition", false);
            }
        }
    }
}