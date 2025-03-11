using UnityEngine;

public class Message : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            Debug.Log("Congratulations! You've reached the goal!");
            // You can also display a UI message here
        }
    }
}
