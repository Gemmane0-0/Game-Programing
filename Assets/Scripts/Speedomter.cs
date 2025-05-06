using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
public class Speedomter : MonoBehaviour
{
    // It references the RigidBody component of the target object (the player's car)
    public Rigidbody target;

    // The maximum speed of the target in mph (miles per hour)
    public float maxSpeed = 0.0f; 

    // The minimum angle for the speedometer arrow
    public float minSpeedArrowAngle;

    // The maximum angle for the speedometer arrow 
    public float maxSpeedArrowAngle;

    // Adds visual separator in Unity Inspector
    [Header("UI")]
    public TMP_Text speedLabel; // The label that displays the speed;
    public RectTransform arrow; // The arrow in the speedometer

    private float speed = 0.0f;
    private void Update()
    {
        // 2.23694f to convert in miles per hour
        // ** The speed must be clamped by the car controller **
        speed = target.velocity.magnitude * 2.23694f;

        if (speedLabel != null)
            speedLabel.text = ((int)speed) + " mph ";
        if (arrow != null)
            arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
    }
}
*/

public class Speedomter : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private GameObject playerObject; // Drag player GameObject here in Inspector
    private PlayerMovement playerMovement;

    [Header("Speedometer Settings")]
    [SerializeField] private float maxSpeed = 120f; // Top speed (mph)
    [SerializeField] private float minSpeedArrowAngle = -90f; // Needle angle at 0 mph
    [SerializeField] private float maxSpeedArrowAngle = 90f; // Needle angle at maxSpeed

    [Header("UI References")]
    [SerializeField] private TMP_Text speedLabel; // Assign TextMeshPro text
    [SerializeField] private RectTransform arrow; // Assign needle UI Image

    private Vector3 lastPosition;
    private float currentSpeed;

    void Start()
    {
        // Get PlayerMovement component automatically
        if (playerObject != null)
        {
            playerMovement = playerObject.GetComponent<PlayerMovement>();
        }

        // Fallback: Try to find player if not assigned
        if (playerMovement == null)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
            if (playerMovement != null)
            {
                playerObject = playerMovement.gameObject;
            }
        }

        // Error if still missing
        if (playerMovement == null)
        {
            Debug.LogError("Speedometer: No PlayerMovement script found!", this);
            enabled = false; // Disable script
            return;
        }

        lastPosition = playerObject.transform.position;
    }

    void Update()
    {
        if (playerMovement == null) return;

        // Calculate speed manually (distance moved per frame)
        Vector3 currentPosition = playerObject.transform.position;
        float distanceMoved = Vector3.Distance(currentPosition, lastPosition);
        currentSpeed = (distanceMoved / Time.deltaTime) * 2.23694f; // Convert to mph

        UpdateSpeedometerUI();
        lastPosition = currentPosition;
    }

    private void UpdateSpeedometerUI()
    {
        // Update speed text
        if (speedLabel != null)
        {
            speedLabel.text = $"{Mathf.RoundToInt(currentSpeed)} mph";
        }

        // Rotate needle
        if (arrow != null)
        {
            float normalizedSpeed = Mathf.Clamp01(currentSpeed / maxSpeed);
            float angle = Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, normalizedSpeed);
            arrow.localEulerAngles = new Vector3(0, 0, -angle); // Negative for clockwise
        }
    }
}
