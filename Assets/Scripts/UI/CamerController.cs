using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour {
    public Transform player;  // Referencia al Transform del jugador.
    public float smoothing = 5f;  // Velocidad de seguimiento de la cámara.
    public Vector3 offset;  // Desplazamiento de la cámara con respecto al jugador.

    private void Start()
    {
        // Verifica si el jugador está asignado, si no, intenta encontrarlo automáticamente.
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    private void LateUpdate()
    {
        // Calcula la nueva posición de la cámara, con el desplazamiento
        Vector3 targetPosition = player.position + offset;

        // Llama al movimiento suave, interpolando entre la posición actual y la deseada
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
