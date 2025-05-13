using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this for scene management

public class Health2 : MonoBehaviour
{
    private int health;
    private int maxHealth = 5; // Max health the player can have
    private bool isGameOver = false; // Add this to track game over state

    // Health bar position and size
    //private Vector2 healthBarPosition = new Vector2(20, 20);
    //private Vector2 healthBarSize = new Vector2(400, 20);

    // Restart button position and size
    private Vector2 restartButtonPosition;
    private Vector2 restartButtonSize = new Vector2(120, 40);
    private bool isCursorLocked = false;

    void Start()
    {
        health = maxHealth; // Initialize health to max health
        isGameOver = false;
        // Position the restart button in the top-right corner with some padding
        restartButtonPosition = new Vector2(Screen.width - restartButtonSize.x - 20, 20);
        
        // Initially unlock cursor in game scene
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isCursorLocked = false;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene" && !isGameOver)
        {
            // Check if player clicked in the center of the screen
            if (Input.GetMouseButtonDown(0))
            {
                // Calculate center area (e.g., middle 20% of screen)
                float centerWidth = Screen.width * 0.2f;
                float centerHeight = Screen.height * 0.2f;
                float centerX = Screen.width / 2 - centerWidth / 2;
                float centerY = Screen.height / 2 - centerHeight / 2;

                // Check if click is in center area
                if (Input.mousePosition.x >= centerX && 
                    Input.mousePosition.x <= centerX + centerWidth &&
                    Input.mousePosition.y >= centerY && 
                    Input.mousePosition.y <= centerY + centerHeight)
                {
                    isCursorLocked = !isCursorLocked;
                    Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
                    Cursor.visible = !isCursorLocked;
                }
            }

            // Check if cursor is over UI elements
            bool isOverRestartButton = Input.mousePosition.x >= restartButtonPosition.x &&
                                     Input.mousePosition.x <= restartButtonPosition.x + restartButtonSize.x &&
                                     Input.mousePosition.y >= Screen.height - restartButtonPosition.y - restartButtonSize.y &&
                                     Input.mousePosition.y <= Screen.height - restartButtonPosition.y;

            // Unlock cursor when over UI
            if (isOverRestartButton)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isCursorLocked = false;
            }
        }
    }

    public void Hurt(int damage)
    {
        if (isGameOver) return; // Don't take damage if game is over
        
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
        isGameOver = true;
        Debug.Log("Player has died!");
        
        // Stop all game actions
        Time.timeScale = 0f;
        
        // Show cursor when game is over
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Disable player movement
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        
        // Disable all scripts except this one
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }

        // Stop all audio
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudioSources)
        {
            audio.Stop();
        }
    }

    // Draw the health bar on the screen
    void OnGUI()
    {
        // Always show restart button in top-right corner
        GUI.color = Color.white;
        if (GUI.Button(new Rect(restartButtonPosition.x, restartButtonPosition.y, restartButtonSize.x, restartButtonSize.y), "Restart"))
        {
            RestartGame();
        }

        // Background bar (gray for health)
        //GUI.color = Color.gray;
        //GUI.Box(new Rect(healthBarPosition.x, healthBarPosition.y, healthBarSize.x, healthBarSize.y), "");

        // Health bar fill (green for full health)
        //GUI.color = Color.green;
        //GUI.Box(new Rect(healthBarPosition.x, healthBarPosition.y, (health / (float)maxHealth) * healthBarSize.x, healthBarSize.y), "");

        // Show game over GUI if the game is over
        if (isGameOver)
        {
            // Create a centered style for the text
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 40;
            style.normal.textColor = Color.red;

            // Display game over text
            GUI.Label(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 100), "GAME OVER", style);

            // Display restart button with larger size and better positioning
            if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 + 50, 200, 60), "Restart Game"))
            {
                RestartGame();
            }
        }
    }

    // New method to handle restarting the game
    private void RestartGame()
    {
        Time.timeScale = 1f;
        isCursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Reload the current scene instead of going back to selection
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
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
