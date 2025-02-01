using System;
using UnityEngine;

namespace Player
{
    public class EnemyInteraction : MonoBehaviour
    {
        
        [Header("Configuracion de Puntos y enemigos")]
        public int cantidadPuntos;
        public LayerMask layerEnemigo;
        
        public ScoreController scoreController;
        private void Update()
        {
            RaycastHit2D enemy = Physics2D.Raycast(transform.position, Vector2.down, 1f, layerEnemigo);
            bool encontrarEnemigo = enemy;
            if (encontrarEnemigo)
            {
                scoreController.score += 300;
                Destroy(enemy.collider.gameObject);
            }
        }
    }
}
