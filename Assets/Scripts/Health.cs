using UnityEngine;
using UnityEngine.SceneManagement; // For reloading the scene

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health
    private int currentHealth; // Player's current health

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with any object that should reduce health
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current scene
    }
}
