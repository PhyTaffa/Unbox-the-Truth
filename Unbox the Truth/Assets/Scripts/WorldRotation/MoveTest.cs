using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float moveSpeed = 0.1f;           // Speed of player movement
    public float jumpForce = 10f;          // Force applied when the player jumps
    public LayerMask groundLayer;          // Layer to identify ground
    public Transform groundCheck;           // Transform to check if the player is grounded
    public float groundCheckRadius = 0.2f; // Radius of ground check

    private Rigidbody2D rb;                // Reference to the Rigidbody2D component
    private bool isGrounded;               // Is the player on the ground?

    protected float moveInput = 0;                   // Horizontal input from the player

    protected int direction = 1;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player
    }

    void Update()
    {
        Move();  // Call the Move function to handle player movement
        Jump();  // Call the Jump function to handle jumping
        testDirection();
    }

    public int getLastDirection()
    {
       if(moveInput*direction > 0)
       {
           return direction;
       }
       else if(moveInput*direction < 0)
       {
           direction = -direction;
       }
       return direction;
    }

    private void testDirection()
    {
        if(getLastDirection() == 1)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }


    private void Move()
    {
        moveInput = Input.GetAxis("Horizontal"); // Get input from the horizontal axis (A/D or Left/Right arrows)

        // Move the player
        //rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.AddForce(new Vector2(moveInput * moveSpeed, 0), ForceMode2D.Impulse);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -5, 5), rb.velocity.y);
    }

    private void Jump()
    {
        // Check if the player is grounded using a circle overlap
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // If the player is grounded and presses the jump button (spacebar)
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Apply an upward force to the player's Rigidbody2D to make them jump
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    // Optional: Visualize the ground check in the Scene view
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
