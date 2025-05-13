using UnityEngine;
using UnityEngine.SceneManagement;

public class Health2 : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;

    private int health;
    private int maxHealth = 5;
    private bool isGameOver = false;

    private Vector2 restartButtonPosition;
    private Vector2 restartButtonSize = new Vector2(120, 40);
    private bool isCursorLocked = false;

    void Start()
    {
        health = maxHealth;
        isGameOver = false;

        // Audio source setup
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        restartButtonPosition = new Vector2(Screen.width - restartButtonSize.x - 20, 20);

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isCursorLocked = false;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene" && !isGameOver)
        {
            float centerWidth = Screen.width * 0.2f;
            float centerHeight = Screen.height * 0.2f;
            float centerX = Screen.width / 2 - centerWidth / 2;
            float centerY = Screen.height / 2 - centerHeight / 2;

            if (Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x >= centerX && Input.mousePosition.x <= centerX + centerWidth &&
                    Input.mousePosition.y >= centerY && Input.mousePosition.y <= centerY + centerHeight)
                {
                    isCursorLocked = !isCursorLocked;
                    Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
                    Cursor.visible = !isCursorLocked;
                }
            }

            bool isOverRestartButton = Input.mousePosition.x >= restartButtonPosition.x &&
                                       Input.mousePosition.x <= restartButtonPosition.x + restartButtonSize.x &&
                                       Input.mousePosition.y >= Screen.height - restartButtonPosition.y - restartButtonSize.y &&
                                       Input.mousePosition.y <= Screen.height - restartButtonPosition.y;

            if (isOverRestartButton)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isCursorLocked = false;
            }
        }
    }

    public void Hurt(int damage)
    {
        if (isGameOver) return;

        health -= damage;
        Debug.Log($"Health: {health}");

        if (damageSound != null)
            audioSource.PlayOneShot(damageSound);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isGameOver = true;
        Debug.Log("Player has died!");

        if (deathSound != null)
            audioSource.PlayOneShot(deathSound);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }

        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudioSources)
        {
            audio.Stop();
        }
    }

    void OnGUI()
    {
        GUI.color = Color.white;
        if (GUI.Button(new Rect(restartButtonPosition.x, restartButtonPosition.y, restartButtonSize.x, restartButtonSize.y), "Restart"))
        {
            RestartGame();
        }

        if (isGameOver)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 40;
            style.normal.textColor = Color.red;

            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "GAME OVER", style);

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 60), "Restart Game"))
            {
                RestartGame();
            }
        }
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        isCursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Hurt(1);
        }
    }
}