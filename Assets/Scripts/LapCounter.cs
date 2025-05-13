using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LapCounter : MonoBehaviour
{
    public int totalLaps = 3;    // Total number of laps for the race
    public TMP_Text lapText;     // UI Text for displaying laps
    public GameObject victoryPanel; // UI Panel for victory screen
    public TMP_Text victoryText;   // Text to display victory message
    public float victoryDisplayTime = 3f; // How long to show victory screen before loading next scene
    public string nextSceneName = "MainMenu"; // Scene to load after victory

    private int currentLap = 0;  // Tracks the current lap
    private bool raceFinished = false;
    private RaceTimer raceTimer; // Reference to the race timer

    private void Start()
    {
        // Find the RaceTimer in the scene
        raceTimer = FindObjectOfType<RaceTimer>();
        
        // Initialize UI
        if (lapText != null)
        {
            lapText.text = "Lap: 0 / " + totalLaps;
        }
        
        // Hide victory panel at start
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the player crosses the finish line
        if (other.CompareTag("FinishLine") && !raceFinished)
        {
            currentLap++;

            // Updates the UI
            if (lapText != null)
            {
                lapText.text = "Lap: " + currentLap + " / " + totalLaps;
            }

            // Check if the race is complete
            if (currentLap >= totalLaps)
            {
                raceFinished = true;
                StartCoroutine(ShowVictorySequence());
            }
        }
    }

    private IEnumerator ShowVictorySequence()
    {
        // Stop the race timer if it exists
        if (raceTimer != null)
        {
            raceTimer.CompleteGame();
        }

        // Show victory panel
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        // Display victory message
        if (victoryText != null)
        {
            victoryText.text = "Victory!\nRace Completed!";
        }

        // Wait for specified time
        yield return new WaitForSeconds(victoryDisplayTime);

        // Load next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
