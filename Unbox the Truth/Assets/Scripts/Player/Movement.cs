
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [Header("SPEED SETTINGS")]
    [SerializeField] public float carryingMovementSpeed = 4f;           // Min speed of player movement
    [SerializeField] public float normalMovementSpeed = 8f;           // Max speed of player movement
    private float moveSpeed = 8f;           //Base move speed
    [SerializeField] public bool isCarryingObject = false;
    
    [Header("JUMP SETTINGS")]
    [SerializeField] public float jumpForce = 11f;          // Force applied when the player jumps
    
    [Header("JUMP CHECK SETTINGS")]
    //Gizmo for jumping
    public LayerMask groundLayer;          // Layer to identify ground
    [SerializeField] private Transform groundCheck;           // Transform to check if the player is grounded
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius of ground check

    private Rigidbody2D _rb;                // Reference to the Rigidbody2D component
    private bool _isGrounded;               // Is the player on the ground?

    
    protected float moveInput = 0;                   // Horizontal input from the player
    protected int direction = 1;
    

    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player
    }

    void Update()
    {
        Move();  // Call the Move function to handle player movement
        Jump();  // Call the Jump function to handle jumping
        testDirection();
    }

    public int getLastDirection()
    {
        if(moveInput * direction > 0)
        { 
           return direction;
        }
        else if(moveInput * direction < 0)
        {
            direction = -direction; 
        }
        return direction;
    }

    private void testDirection()
    {
        if(getLastDirection() == 1)
        {
            //GetComponent<SpriteRenderer>().color = Color.yellow;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            //GetComponent<SpriteRenderer>().color = Color.red;
            
        }
    }
    
    private void Move()
    {
        moveInput = Input.GetAxis("Horizontal"); // Get input from the horizontal axis (A/D or Left/Right arrows)

        if (!isCarryingObject)
        {
            moveSpeed = normalMovementSpeed;
        }
        else
        {
            moveSpeed = carryingMovementSpeed;
        }
        
        // Move the player

        if (_isGrounded)
        {
            _rb.velocity = new Vector2(moveInput * moveSpeed, _rb.velocity.y);
            return;
        }


        //if (_rb.velocity.magnitude < moveSpeed || Mathf.Sign(_rb.velocity.x) != Mathf.Sign(moveInput) )
        {
            _rb.AddForce(new Vector2(moveInput * 3, 0), ForceMode2D.Force);
        }
        //_rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -5, 5), _rb.velocity.y);

        
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    private void Jump()
    {

        // Check if the player is grounded using a circle overlap
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Debugging output
       // Debug.Log("Is Grounded: " + _isGrounded);

        // If the player is grounded and presses the jump button (space)
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            // Apply an upward force to the player's Rigidbody2D to make them jump
            _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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
