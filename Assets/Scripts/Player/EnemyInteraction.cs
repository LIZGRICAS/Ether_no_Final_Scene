using System;
using UnityEngine;


namespace Player
{
    public class EnemyInteraction : MonoBehaviour
    {
        
        [Header("Configuracion de Puntos y enemigos")]
        public int cantidadPuntos;
        public LayerMask Enemy;
        private PlayerController controlador;
        
        public ScoreController scoreController;

        private void Awake()
        {
            controlador = GetComponent<PlayerController>();
        }

        private void RecibirDaño()
        {
            // animaciones
            //efectos
            controlador.QuitarMana(20);
        }
        
        private void Atacar()
        {
            // animaciones
            //efectos
                scoreController.health += 30;
               // Destroy(enemy.collider.gameObject);
            
        }
    }
}
