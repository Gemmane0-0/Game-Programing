using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;

    public float speed = 1.5f; //player speed
    public float boostSpeed = 3f; //speed during the boost
    public float boostDuration = 2f; // duration of speed boost
    public float boostCooldown = 5f; //cooldown of the boost after used
    public ParticleSystem boostParticles;


    private CharacterController charController;
    private bool isBoosting = false;
    private float boostEndTime = 0f;
    private float nextBoostTime = 0f;

    // Start is called before the first frame update
    void Start() {
        charController = GetComponent<CharacterController>();

        _particles.enableEmission = false;

        //make sure particles come out when boosting only
        if (boostParticles != null) {
            boostParticles.Stop();
        }

    }

    // Update is called once per frame
    void Update(){

        HandleMovement();
        HandleBoost();
    }

    void HandleMovement() {

        float deltaX = Input.GetAxis("Horizontal") * (isBoosting ? boostSpeed : speed);
        float deltaZ = Input.GetAxis("Vertical") * (isBoosting ? boostSpeed : speed);

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, isBoosting ? boostSpeed : speed);

        movement *= Time.deltaTime;

        charController.Move(movement);

        transform.Translate(deltaX, 0, deltaZ);
    }

    void HandleBoost() {
        //use spacebar to activate boost
        if (Input.GetKeyDown(KeyCode.Space)) {

            TryActivateBoost();
        }

        //check if boost should end
        if (isBoosting && Time.time >= boostEndTime) {
            EndBoost();
        }

    }

    void TryActivateBoost() {
        if (Time.time >= nextBoostTime) {
            StartBoost();
        }
    }

    void StartBoost() {
        isBoosting = true;
        boostEndTime = Time.time + boostDuration;
        nextBoostTime = Time.time + boostDuration + boostCooldown;

        //Play particles
        if (boostParticles != null) {
            boostParticles.Play();
        }
    }

    void EndBoost() {
        isBoosting = false;

        if (boostParticles != null) {
            boostParticles.Stop();
        }
    }
}
