using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Image healthBar; // Reference to the UI Image for the health bar

    private int collisionCount = 0;
    public int maxCollisions = 3; // Number of times the player can hit an obstacle before resetting
    private Vector3 startPosition; // Stores the starting position of the player

    void Start()
    {
        currentHealth = maxHealth;
        startPosition = transform.position; // Save initial position
        UpdateHealthUI();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(10); // Adjust damage as needed
            collisionCount++;

            if (collisionCount >= maxCollisions)
            {
                ResetPlayer();
            }
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // Shrink the bar based on health
        }
    }

    void ResetPlayer()
    {
        Debug.Log("Player Reset to Start Position!");
        transform.position = startPosition; // Move player back to starting position
        currentHealth = maxHealth; // Reset health
        collisionCount = 0; // Reset collision count
        UpdateHealthUI();
    }
}
