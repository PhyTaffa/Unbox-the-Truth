using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement parameters
    public float acceleration = 5f;   // How quickly the player speeds up
    public float maxSpeed = 10f;      // Maximum speed
    public float deceleration = 5f;   // How quickly the player slows down when the key is released
    public float jumpForce = 10f;     // Jump force
    public LayerMask groundLayer;     // Layer to check if player is on the ground

    // To display the velocity in the inspector
    [SerializeField] public Vector2 currentVelocity;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Prevent rotation during movement
    }

    private void Update()
    {
        // Update the velocity shown in the Inspector (to see it constantly)
        currentVelocity = rb.velocity;

        // Check if player is grounded (for jumping)
        isGrounded = CheckIfGrounded();

        // Handle movement (horizontal only)
        HandleMovement();

        // Handle jumping (spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Draw the ray in the Scene view (only for debugging)
        DrawRaycast();
    }

    private bool CheckIfGrounded()
    {
        // Raycast downward to check if the player is on the ground
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
    }

    private void HandleMovement()
    {
        // Get input for horizontal movement (left-right)
        float horizontal = Input.GetAxis("Horizontal");

        // If there is input, build up speed, otherwise decelerate
        if (Mathf.Abs(horizontal) > 0f)
        {
            // Apply acceleration towards the target velocity
            velocity.x = Mathf.MoveTowards(velocity.x, horizontal * maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            // If no input, apply deceleration (slow down)
            velocity.x = Mathf.MoveTowards(velocity.x, 0f, deceleration * Time.deltaTime);
        }

        // Apply the velocity to the Rigidbody2D (no change to the Y velocity)
        rb.velocity = new Vector2(velocity.x, rb.velocity.y);
    }

    private void Jump()
    {
        // Apply upward force for jump (if grounded)
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Directly set the y velocity to apply jump
    }

    // Optional: Prevent the player from sticking to walls
    private void OnCollisionStay2D(Collision2D other)
    {
        // If we are not grounded, and the player is still moving against a wall, stop the horizontal movement
        if (!isGrounded)
        {
            // Prevent sliding on walls by applying zero horizontal velocity when in the air
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    // Debug function to draw the raycast in the Scene view
    private void DrawRaycast()
    {
        // Color of the ray (red if grounded, green if not grounded)
        Color rayColor = isGrounded ? Color.green : Color.red;
        
        // Draw the ray from the player's position downward, and visualize the ray length
        Debug.DrawRay(transform.position, Vector2.down * 0.1f, rayColor);
    }
}
