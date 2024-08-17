using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.18f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    public float maxHealth = 100f;  // Sức khỏe tối đa
    private float currentHealth;    // Sức khỏe hiện tại
    public Slider healthBar;        // Thanh máu trên UI


    public string gameOverSceneName = "GameOver";


    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth; // Đặt sức khỏe ban đầu
        healthBar.maxValue = maxHealth; // Đặt giá trị tối đa cho thanh máu
        healthBar.value = currentHealth; // Đặt giá trị ban đầu cho thanh máu
    }

    void Update()
    {
        // Groundcheck
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Resetting the default velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Getting the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Creating the moving vector
        Vector3 move = transform.right * x + transform.forward * z; // (right - red axis, forward - blue axis)

        // Actually moving the player
        controller.Move(move * speed * Time.deltaTime);

        // Check if the player can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Going up
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Falling down
        velocity.y += gravity * Time.deltaTime;

        // Executing the jump
        controller.Move(velocity * Time.deltaTime);

        // Checking if the player is moving
        if (lastPosition != gameObject.transform.position && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = gameObject.transform.position;
    }

    // Function to take damage
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    
    void Die()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}
