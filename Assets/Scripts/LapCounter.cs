using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapCounter : MonoBehaviour
{
    public int totalLaps = 3;    // Total number of laps for the race
    public TMP_Text lapText;         // UI Text for displaying laps

    private int currentLap = 0;  // Tracks the current lap
    private bool raceFinished = false;

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
                Debug.Log("Race Finished!");
                // Adds race completion logic here )
            }
        }
    }
}
