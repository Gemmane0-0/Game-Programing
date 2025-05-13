using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private ParticleSystem _particles;
    [Header("UI Elements")]
    [SerializeField] private Image boostCooldownImage;
    [SerializeField] private GameObject boostReadyIndicator;
    [SerializeField] private Text cooldownText; // Optional text display
    [SerializeField] private Animator boostUIAnimator; // Optional animations

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

        InitializeUI();
    }

    void InitializeUI()
    {
        if (boostCooldownImage != null)
        {
            boostCooldownImage.fillAmount = 1;
        }

        if (boostReadyIndicator != null)
        {
            boostReadyIndicator.SetActive(true);
        }

        if (cooldownText != null)
        {
            cooldownText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update(){

        HandleMovement();
        HandleBoost();
        UpdateBoostUI();
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

    void UpdateBoostUI()
    {
        if (Time.time < nextBoostTime)
        {
            // Boost on cooldown
            float remainingCooldown = nextBoostTime - Time.time;
            float cooldownProgress = 1 - (remainingCooldown / boostCooldown);

            if (boostCooldownImage != null)
            {
                boostCooldownImage.fillAmount = cooldownProgress;
            }

            if (boostReadyIndicator != null)
            {
                boostReadyIndicator.SetActive(false);
            }

            if (cooldownText != null)
            {
                cooldownText.gameObject.SetActive(true);
                cooldownText.text = Mathf.Ceil(remainingCooldown).ToString();
            }
        }
        else
        {
            // Boost ready
            if (boostCooldownImage != null)
            {
                boostCooldownImage.fillAmount = 1;
            }

            if (boostReadyIndicator != null)
            {
                boostReadyIndicator.SetActive(true);
            }

            if (cooldownText != null)
            {
                cooldownText.gameObject.SetActive(false);
            }

            // Optional: Pulse effect when ready
            if (!isBoosting && boostUIAnimator != null)
            {
                boostUIAnimator.SetBool("Ready", true);
            }
        }

        // Optional: Different visual during active boost
        if (isBoosting && boostUIAnimator != null)
        {
            boostUIAnimator.SetBool("Boosting", true);
        }
        else if (boostUIAnimator != null)
        {
            boostUIAnimator.SetBool("Boosting", false);
        }
    }

}