using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health2 : MonoBehaviour
{
    private int health;
    private int maxHealth = 5; // Max health the player can have

    // Health bar position and size
    private Vector2 healthBarPosition = new Vector2(20, 20);
    private Vector2 healthBarSize = new Vector2(400, 20);

    void Start()
    {
        health = maxHealth; // Initialize health to max health
    }

    public void Hurt(int damage)
    {
        health -= damage;
        Debug.Log($"Health: {health}");

        // Check if the player has died
        if (health <= 0)
        {
            Die();
        }
    }

    // This method is called when the player dies
    void Die()
    {
        Debug.Log("Player has died!");
        // Add your death logic here, e.g., disable player movement, trigger game over, etc.
    }

    // Draw the health bar on the screen
    void OnGUI()
    {
        // Background bar (gray for health)
        GUI.color = Color.gray;
        GUI.Box(new Rect(healthBarPosition.x, healthBarPosition.y, healthBarSize.x, healthBarSize.y), "");

        // Health bar fill (green for full health)
        GUI.color = Color.gray;
        GUI.Box(new Rect(healthBarPosition.x, healthBarPosition.y, (health / (float)maxHealth) * healthBarSize.x, healthBarSize.y), "");
    }


    // This is where you detect collisions with obstacles (other triggers)
    void OnTriggerEnter(Collider other)
    {
        // If the player collides with an obstacle, take damage (e.g., damage value is 1)
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Hurt(1);
        }
    }
}
