using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Add TextMeshPro support

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject finishMessage; // UI element to show finish message
    [SerializeField] private GameObject completionScreen; // UI element for the completion screen
    [SerializeField] private TextMeshProUGUI congratulationsText; // Reference to the congratulations text
    [SerializeField] private float messageDisplayTime = 3f; // How long to show the message
    private bool hasFinished = false;

    private void Start()
    {
        if (finishMessage != null)
        {
            finishMessage.SetActive(false);
        }
        if (completionScreen != null)
        {
            completionScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFinished)
        {
            if (Checkpoint.AreAllCheckpointsActivated())
            {
                FinishLevel();
            }
            else
            {
                // Show message that player needs to hit all checkpoints
                Debug.Log("You need to hit all checkpoints first!");
                // You can add UI feedback here
            }
        }
    }

    private void FinishLevel()
    {
        hasFinished = true;
        Debug.Log("Level Complete!");

        // Freeze the entire world
        Time.timeScale = 0f;

        // Stop all moving objects
        Rigidbody[] allRigidbodies = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in allRigidbodies)
        {
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }

        // Stop all audio
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudioSources)
        {
            if (audio != null)
            {
                audio.Pause();
            }
        }

        // Show congratulations message
        if (completionScreen != null)
        {
            completionScreen.SetActive(true);
            
            // Set congratulations text
            if (congratulationsText != null)
            {
                congratulationsText.text = "CONGRATULATIONS!\nYou've completed the level!";
                congratulationsText.color = Color.yellow; // Make it stand out
            }
        }

        // Reset checkpoints
        Checkpoint.ResetCheckpoints();
    }

    // Call this method from a UI button
    public void RestartGame()
    {
        // Unfreeze the world
        Time.timeScale = 1f;
        
        // Resume all audio
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudioSources)
        {
            if (audio != null)
            {
                audio.UnPause();
            }
        }

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Call this method from a UI button
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
} 