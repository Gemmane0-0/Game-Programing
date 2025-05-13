using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber; // The order number of this checkpoint
    private bool isActivated = false;
    private static int totalCheckpoints = 0;
    private static int activatedCheckpoints = 0;

    private void Start()
    {
        totalCheckpoints++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            activatedCheckpoints++;
            
            // Visual feedback that checkpoint was activated
            GetComponent<Renderer>().material.color = Color.green;
            
            Debug.Log($"Checkpoint {checkpointNumber} activated! ({activatedCheckpoints}/{totalCheckpoints})");
        }
    }

    public static bool AreAllCheckpointsActivated()
    {
        return activatedCheckpoints >= totalCheckpoints;
    }

    public static void ResetCheckpoints()
    {
        activatedCheckpoints = 0;
        totalCheckpoints = 0;
    }
} 