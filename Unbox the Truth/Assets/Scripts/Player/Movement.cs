
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
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
    [SerializeField] internal float groundCheckRadius = 0.05f; // Radius of ground check

    private Rigidbody2D _rb;                // Reference to the Rigidbody2D component
    private bool _isGrounded;               // Is the player on the ground?

    
    protected float moveInput = 0;                   // Horizontal input from the player
    protected int direction = 1;

    private bool IsHiding = false;
    private SpriteRenderer playerSprite;

    [Header("Sprites")]
    [SerializeField] private Sprite HidingSprite;
    [SerializeField] private Sprite DefaultSprite;
    private bool _isOnPlatform;

    private bool usePlatformMechanics;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player
        playerSprite = GetComponent<SpriteRenderer>();
        
        
        //debug
        // if (SpriteManager.selectedSprite != null)
        // {
        //     
        // }
    }

    void Update()
    {
        Hide();
        
        if(!IsHiding && !usePlatformMechanics)
        {
            Move();  // Call the Move function to handle player movement
            Jump();  // Call the Jump function to handle jumping
            testDirection();
        }else if(usePlatformMechanics)
        {
            //Debug.Log("Use Platform Mechanics");
            //PlatformMove();
            //PlatformJump();
            //testDirection();
        }
        
    }

    /*private void PlatformJump()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            notEnter = true;
            Debug.Log("Keydown working");
            Vector2 playerPosOld = transform.position;
            transform.position += Vector3.up*0.1f;
            Vector2 playerPosNew = transform.position;
            _rb.simulated = true;
            transform.parent = null;
            transform.localScale = new Vector3(1,1,1);
            SetUsePlatformMechanics(true);
            SetIsGrounded(false);
            _rb.velocity = (playerPosNew - playerPosOld)*100;
            float limitedYVel = Mathf.Clamp(platformRB.velocity.y, 0, 100);
            playerRB.velocity = new Vector2(platformRB.velocity.x, playerRB.velocity.y + limitedYVel);
            //playerRB.velocity = new Vector2(10, playerRB.velocity.y);
            Debug.Log(playerRB.velocity);
            //if(jump == null){
                //jump = StartCoroutine(Jump());
            //}
        }
    }

    private void PlatformMove()
    {
        if(Input.GetKey(KeyCode.G))
        {
            transform.position -= transform.right * Time.deltaTime * moveSpeed;
        }

        if(Input.GetKey(KeyCode.H))
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
        }
    }
    */

    private void Hide()
    {
        // if(Input.GetKey(KeyCode.LeftShift))
        // {
        //     IsHiding = true;
        //     playerSprite.sprite = HidingSprite;
        //
        // }else{
        //     IsHiding = false;
        //     playerSprite.sprite = DefaultSprite;
        // }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsHiding = true;
            playerSprite.sprite = HidingSprite;
        }else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsHiding = false;
            playerSprite.sprite = SpriteManager.selectedSprite;
        }
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

    public void testDirection()
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

    public void flipSprite(int direction){
        if(direction == 1)
        {
            //GetComponent<SpriteRenderer>().color = Color.yellow;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(direction == -1)
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

        if (_isGrounded && !usePlatformMechanics)
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
        if (Input.GetButtonDown("Jump"))
        {   
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            if(!_isGrounded)
            {
                return;
            }
            //_rb.simulated = true;
            _rb.velocity = Vector2.zero;
            // Apply an upward force to the player's Rigidbody2D to make them jump
            _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    public bool GetIsHiding(){
        return IsHiding;
    }

    public void SetUsePlatformMechanics(bool value){
        usePlatformMechanics = value;
    }

    public void SetIsGrounded(bool value){
        _isGrounded = value;
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
