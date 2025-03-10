using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI_2 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;
    public float movementDistance = 3f;
    public float frequency = 2f;
    public float obstacleRange = 2f;

    [Header("Combat Settings")]
    public int damageAmount = 10;

    private bool isAlive = true;
    private float startY;
    private Rigidbody rb;
    private bool movingUp = true;

    private void Start()
    {
        isAlive = true;
        startY = transform.position.y;

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (isAlive)
        {
            float direction = movingUp ? 1 : -1;
            float newY = startY + Mathf.Sin(Time.time * frequency) * movementDistance * direction;
            Vector3 targetPosition = new Vector3(transform.position.x, newY, transform.position.z);

            // Smooth movement
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);

            HandleDetection(direction);
        }
    }

    private void HandleDetection(float direction)
    {
        Vector3 castDirection = direction > 0 ? transform.up : -transform.up;
        Ray ray = new Ray(transform.position, castDirection);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.5f, out hit, obstacleRange))
        {
            GameObject hitObject = hit.transform.gameObject;

            // ✅ Use SendMessage for player interaction (no need for public access)
            if (hitObject.CompareTag("Player"))
            {
                hitObject.SendMessage("TakeDamage", damageAmount, SendMessageOptions.DontRequireReceiver);
            }
            else if (hitObject.CompareTag("Obstacle"))
            {
                ReverseDirection();
            }
        }
    }

    private void ReverseDirection()
    {
        movingUp = !movingUp;

        float targetXRotation = movingUp ? 0f : 180f;
        StartCoroutine(SmoothRotate(targetXRotation));
    }

    private IEnumerator SmoothRotate(float targetXRotation)
    {
        float timeElapsed = 0f;
        float duration = 0.2f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(targetXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        while (timeElapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}
