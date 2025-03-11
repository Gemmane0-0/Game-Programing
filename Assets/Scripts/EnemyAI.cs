using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {

    public float speed = 3f;
    public float movementDistance = 3f;
    public float frequency = 2f;
    public float obstacleRange = 2f;
    public int damageAmount = 10;

    private bool isAlive = true;
    private float startX;
    private Rigidbody rb;

    private void Start() {
        isAlive = true;
        startX = transform.position.x;

        // Add Rigidbody for collision handling
        rb = GetComponent<Rigidbody>();
        if (rb == null) {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
        rb.freezeRotation = true;
    }

    void Update() {
        if (isAlive) {
            // Left-to-right movement using sine wave
            float newX = startX + Mathf.Sin(Time.time * frequency) * movementDistance;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        // Obstacle and player detection using SphereCast
        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.5f, out hit, obstacleRange)) {
            GameObject hitObject = hit.transform.gameObject;

            // If the enemy detects a player → Deal damage using SendMessage()
            if (hitObject.CompareTag("Player")) {
                hitObject.SendMessage("TakeDamage", damageAmount, SendMessageOptions.DontRequireReceiver);
            } 
            // If the enemy detects an obstacle → Reverse direction
            else if (hitObject.CompareTag("Obstacle")) {
                movementDistance = -movementDistance;
                float angle = Random.Range(-45, 45);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    public void SetAlive(bool alive) {
        isAlive = alive;
    }
}
