using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
