using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RaceTimer : MonoBehaviour
{
    public TMP_Text timerText;  // Assign the TimerText UI object in the inspector
    private float timeRemaining = 60.0f;  // 1 minute countdown
    private bool isRunning = false;
    public string gameOverSceneName = "GameOver";  // Scene to load when time runs out
    public bool gameEnded = false;

    void Start()
    {
        isRunning = true; // Start the timer when the scene loads
        timeRemaining = 60.0f;
    }

    void Update()
    {
        if (isRunning && !gameEnded)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                GameOver();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        int milliseconds = Mathf.FloorToInt((timeRemaining * 100) % 100);

        // Make the timer text red when less than 10 seconds remaining
        if (timeRemaining <= 10)
        {
            timerText.color = Color.red;
        }

        timerText.text = $"Time Left: {minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    void GameOver()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            isRunning = false;
            Debug.Log("Game Over - Time's Up!");
            
            // You can either load a game over scene
            if (!string.IsNullOrEmpty(gameOverSceneName))
            {
                SceneManager.LoadScene(gameOverSceneName);
            }
            // Or implement your own game over logic here
            // For example, show a game over UI panel
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        timeRemaining = 60.0f;
        gameEnded = false;
        timerText.color = Color.white;
        UpdateTimerDisplay();
    }

    // Call this when player completes the game successfully
    public void CompleteGame()
    {
        isRunning = false;
        gameEnded = true;
        // Add any victory logic here
    }
}
