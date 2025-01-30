using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MovementInstructions : MonoBehaviour
    {
        
        public Text signInstructionGame;   // Letrero "Instrucciones de controles del juego"

        void Start()
        {
            // Inicializa 
            signInstructionGame.gameObject.SetActive(false);

            // Al principio, mostramos el letrero "Instrucciones de controles del juego" por 3 segundos
            signInstructionGame.gameObject.SetActive(true);
            Invoke("HideInstructions", 3f); // El letrero de "Let's Go" se oculta después de 3 segundos
        }

        // Método para ocultar el letrero 
        void HideInstructions()
        {
            signInstructionGame.gameObject.SetActive(false); // Ocultamos el letrero
        }
    }
}