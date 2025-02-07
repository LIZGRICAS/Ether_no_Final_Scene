using UnityEngine;
using UnityEngine.UI; // Para usar Button
using UnityEngine.SceneManagement; // Necesario para cargar escenas

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        public GameObject canvasBarLives;// el Canvas que se debe ocultar
        public GameObject canvasMenu; // El primer Canvas que se debe mostrar
        public GameObject canvasControls; // El segundo Canvas que se debe mostrar
        public Button startButton; // El botón que activa el cambio

        // Botones del Game Over
        public Button restartButton; // Botón de reiniciar
        public Button exitButton; // Botón de salir
        private ScoreController scoreController; // Referencia al controlador de puntaje y juego

        void Start()
        {
            // Asigna el evento de clic al botón de inicio
            startButton.onClick.AddListener(OnStartButtonClicked);
            
            // Referencia al ScoreController (asegúrate de asignarlo desde el Inspector o buscarlo en la escena)
            scoreController = FindObjectOfType<ScoreController>(); // Busca el ScoreController en la escena

            // Asigna los eventos de clic a los botones de reiniciar y salir
            if (restartButton != null)
                restartButton.onClick.AddListener(RestartGame);

            if (exitButton != null)
                exitButton.onClick.AddListener(ExitGame);
        }

        // Método que se llama cuando se presiona el botón de "Start"
        void OnStartButtonClicked()
        {
            // Desactivo el canvas del menu
            if (canvasMenu != null)
            {
                canvasMenu.SetActive(false);
            }

            // Activa el canvas de los controles
            if (canvasControls != null)
            {
                canvasControls.SetActive(true);
            }
            
            // Activa el canvas de las barras de vida y salud
            if (canvasBarLives != null)
            {
                canvasBarLives.SetActive(true);
            }
        }

        // Método para reiniciar el juego
        void RestartGame()
        {
            // Llamamos al método de reiniciar desde el ScoreController
            if (scoreController != null)
            {
                scoreController.RestartGame(); // Reinicia el juego
            }

            // Opcional: reiniciar escena si es necesario
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
        }

        // Método para salir del juego
        void ExitGame()
        {
            // Salir del juego
            Application.Quit();
            Debug.Log("Game is exiting...");
        }

    }
}


