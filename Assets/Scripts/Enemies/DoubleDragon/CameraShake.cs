using UnityEngine;

namespace Enemys.DoubleDragon
{
    public class CameraShake : MonoBehaviour
    {
        // Variables públicas para ajustar el temblor
        public float shakeMagnitude = 0.5f;  // Magnitud del temblor
        public float shakeDuration = 0.7f;   // Duración del temblor

        private Vector3 originalPosition;     // Posición original de la cámara
        private float shakeTime = 0f;         // Temporizador para la duración del temblor

        // Método para iniciar el temblor de la cámara
        public void StartShake(float magnitude, float duration)
        {
            shakeMagnitude = magnitude;
            shakeDuration = duration;
            originalPosition = transform.position;  // Guarda la posición original de la cámara
            shakeTime = duration;  // Inicia el temporizador para el temblor
        }

        private void Update()
        {
            if (shakeTime > 0f) 
            {
                transform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
                shakeTime -= Time.deltaTime; // Reduce el tiempo restante del temblor

                if (shakeTime <= 0f)
                {
                    transform.position = originalPosition;  // Restablece la posición original de la cámara
                }
            }
        }
    }
}