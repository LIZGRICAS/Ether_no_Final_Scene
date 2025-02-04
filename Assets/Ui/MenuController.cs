using UnityEngine;
using UnityEngine.UI; // Para usar Button

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        public GameObject canvasToHide; // El Canvas que se debe ocultar
        public GameObject canvasToShow1; // El primer Canvas que se debe mostrar
        public GameObject canvasToShow2; // El segundo Canvas que se debe mostrar
        public Button startButton; // El botón que activa el cambio
        
        void Start()
        {
            // Asigna el evento de clic al botón
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        
        // Método que se llama cuando se presiona el botón
        void OnStartButtonClicked()
        {
            // Desactiva el Canvas que se debe ocultar
            if (canvasToHide != null)
            {
                canvasToHide.SetActive(false);
            }

            // Activa los dos Canvas que se deben mostrar
            if (canvasToShow1 != null)
            {
                canvasToShow1.SetActive(true);
            }

            if (canvasToShow2 != null)
            {
                canvasToShow2.SetActive(true);
            }
        }
    }
}

