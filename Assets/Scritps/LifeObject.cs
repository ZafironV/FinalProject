using UnityEngine;

public class LifeObject : MonoBehaviour
{
    private GameManager gameManager;

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.IncreaseLinkLives(); // Incrementa las vidas del jugador en el GameManager
            Destroy(gameObject); // Destruye este objeto de vida
        }
    }
}
