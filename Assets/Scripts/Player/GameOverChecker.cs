using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverChecker : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            print("Congratulations, You win!");
        }
        if (collision.gameObject.CompareTag("Dead"))
        {
            print("Game Over");
            Destroy(gameObject);
            
            SceneManager.LoadScene("GameOver");
        }
    }
}
