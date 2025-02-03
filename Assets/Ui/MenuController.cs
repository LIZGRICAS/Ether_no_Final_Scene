using UnityEngine;
using UnityEngine.UI; // Para usar Button

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        public GameObject canvasToHide; // Arrastra tu Canvas aquí desde el inspector
        public Button startButton; // Arrastra tu botón aquí
        // Esta función debe ser pública para que sea accesible en el inspector de Unity
        void Start()
        {
            // Asigna el evento de clic del botón
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        
        // Método que se llama cuando se presiona el botón
        void OnStartButtonClicked()
        {
            if (canvasToHide != null)
            {
                canvasToHide.SetActive(false); // Oculta el canvas
            }
        }
    }
}
