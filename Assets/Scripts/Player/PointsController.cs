using System;
using UnityEngine;

public class PointsController : MonoBehaviour
{
   public ScoreController scoreController;

   private void OnTriggerEnter2D(Collider2D collision)
   {
       // valida si el objeto con el cual se colisiono es una gema y se aumenta 20 puntos en salud
       if (collision.gameObject.CompareTag("Gem"))
       {
           scoreController.health += 20;
           Destroy(collision.gameObject);
       }
       // valida si el objeto con el cual se colisiono es una gema y se aumenta 20 puntos en salud
       if (collision.gameObject.CompareTag("Sword"))
       {
           scoreController.Heal(1);
           Destroy(collision.gameObject);
       }
}   
}
