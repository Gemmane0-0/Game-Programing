/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WanderingAI : MonoBehaviour
{
    //projectile to shoot
    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;
    //speed
    public float speed = 3f;
    public float obstacleRange = 5f;
    //gameobject as a bool
    private bool isAlive;
    private void Start()
    {
        isAlive = true; // ensures gameobject is alive
    }
    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            // Move forward
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        // Create a ray in the same direction as the game object's direction of
        movement
        Ray ray = new Ray(transform.position, transform.forward);
        // Perform a sphere cast
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            // Get a reference to the game object hit by the spherecast
            GameObject hitObject = hit.transform.gameObject;
            // If the object hit was the player, shoot a fireball at the player
            // Otherwise, if the object is within the obstacle range, turn around
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (fireball == null)
                {
                    fireball = Instantiate(fireballPrefab) as GameObject;
                    fireball.transform.position =
                    transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                }
            }
            else if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }
    // Public method to set isAlive
    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
    // Additional functionality: Increase speed over time
    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }
    // Additional method to reset fireball
    public void ResetFireball()
    {
        fireball = null;
    }
}
*/ //