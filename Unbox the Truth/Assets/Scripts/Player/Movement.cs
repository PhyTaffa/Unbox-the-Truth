using System;
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

    private BoxCollider2D playerBoxCollider;

    private float playerBoxColliderXOffset;

    private PlayerSoundPlayer psp;
    private bool wasGrounded = true; // Tracks the previous grounded state
    private bool wasMoving = false; // Tracks the previous movement state

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player
        playerSprite = GetComponent<SpriteRenderer>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        playerBoxColliderXOffset = playerBoxCollider.offset.x;

        playerBoxCollider.enabled = true;
        psp = GetComponent<PlayerSoundPlayer>();
    }

    void Update()
    {
        Hide();
        
        if(!IsHiding && !usePlatformMechanics)
        {
            HandleMovementSound();
                
            Move();  // Call the Move function to handle player movement
            Jump();  // Call the Jump function to handle jumping
            ClampSpeed();
            testDirection();
            
            HandleLandingSound();
        }else if(usePlatformMechanics)
        {
            //Debug.Log("Use Platform Mechanics");
            //PlatformMove();
            //PlatformJump();
            //testDirection();
        }
        
    }

    private void ClampSpeed()
    {
        
    }

    private void Hide()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsHiding = true;
            playerSprite.sprite = HidingSprite;
            psp.PlaySpecificSound(PlayerSoundPlayer.Action.StartDisguise);
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsHiding = false;
            playerSprite.sprite = SpriteManager.selectedSprite;
            psp.PlaySpecificSound(PlayerSoundPlayer.Action.StopDisguise);
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
            if(isCarryingObject){
                IBox box = GetComponentInChildren<IBox>();
                box.GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            }
            direction = -direction; 
        }
        return direction;
    }

    public void testDirection()
    {
        flipSprite(getLastDirection());
    }

    public void flipSprite(int direction){
        if(direction == 1)
        {
            //GetComponent<SpriteRenderer>().color = Color.yellow;
            GetComponent<SpriteRenderer>().flipX = false;
            playerBoxCollider.offset = new Vector2(playerBoxColliderXOffset, playerBoxCollider.offset.y);
            if(isCarryingObject){
                IBox box = GetComponentInChildren<IBox>();
                box.GetComponent<BoxCollider2D>().offset = new Vector2(playerBoxColliderXOffset, box.GetComponent<BoxCollider2D>().offset.y);
            }
        }
        else if(direction == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            playerBoxCollider.offset = new Vector2(-playerBoxColliderXOffset, playerBoxCollider.offset.y);
            if(isCarryingObject){
                IBox box = GetComponentInChildren<IBox>();
                box.GetComponent<BoxCollider2D>().offset = new Vector2(-playerBoxColliderXOffset, box.GetComponent<BoxCollider2D>().offset.y);
            }
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

        // if (Math.Abs(moveInput) == 0)
        // {
        //     if (isNotMoving)
        //     {
        //         psp.StopOnLoop(psp.Action.StopMove);
        //     }
        //     else
        //     {
        //         isNotMoving = false;
        //     }
        //     
        // }
        
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
    
    private void HandleMovementSound()
    {
        bool isMoving = Mathf.Abs(moveInput) > 0.01f && _isGrounded; // Check if the player is moving and grounded

        if (isMoving && !wasMoving)
        {
            // Player started moving
            psp.PlayOnLoop(PlayerSoundPlayer.Action.StartMove);
        }
        else if (!isMoving && wasMoving)
        {
            // Player stopped moving
            psp.StopOnLoop(PlayerSoundPlayer.Action.StartMove);
            //psp.PlaySpecificSound(playerSoundPlayer.Action.StopMove);
        }

        wasMoving = isMoving;
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
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {   
            //_rb.simulated = true;
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            
            // Apply an upward force to the player's Rigidbody2D to make them jump
            _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            
            psp.PlaySpecificSound(PlayerSoundPlayer.Action.Jump);
        }
    }
    
    private void HandleLandingSound()
    {
        bool isCurrentlyGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (!wasGrounded && isCurrentlyGrounded)
        {
            // Player landed
            psp.PlaySpecificSound(PlayerSoundPlayer.Action.Land);
        }

        wasGrounded = isCurrentlyGrounded;
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
