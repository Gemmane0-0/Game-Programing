using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Message : MonoBehaviour
{
    public GameObject victoryPanel; // UI Panel for victory screen
    public TMP_Text victoryText;   // Text to display victory message
    public float victoryDisplayTime = 3f; // How long to show victory screen before loading next scene
    public string nextSceneName = "MainMenu"; // Scene to load after victory

    private void Start()
    {
        // Hide victory panel at start
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            Debug.Log("Congratulations! You've reached the goal!");
            StartCoroutine(ShowVictorySequence());
        }
    }

    private IEnumerator ShowVictorySequence()
    {
        // Pause the game
        Time.timeScale = 0f;

        // Show victory panel
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        // Display victory message
        if (victoryText != null)
        {
            victoryText.text = "Congratulations!\nYou've reached the goal!";
        }

        // Wait for specified time (using unscaled time since game is paused)
        yield return new WaitForSecondsRealtime(victoryDisplayTime);

        // Resume time scale before loading next scene
        Time.timeScale = 1f;

        // Load next scene
        SceneManager.LoadScene(nextSceneName);
    }
}

