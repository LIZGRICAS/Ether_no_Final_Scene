using System;
using UnityEngine;

public class PointsController : MonoBehaviour
{
   public ScoreController scoreController;

   private void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.gameObject.CompareTag("Gem"))
       {
           scoreController.score += 100;
           Destroy(collision.gameObject);
       }
}
}
