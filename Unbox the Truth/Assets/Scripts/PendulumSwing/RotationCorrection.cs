using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class RotationCorrection : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    private GameObject player;
    private Movement playerMovement;
    private Rigidbody2D playerRB;

    private Rigidbody2D platformRB;

    private bool leftPlatform;
    private Vector2 pendulumUp;
    private Vector2 pendulumStart;
    private HingeJoint2D hingeJoint2D;
    [SerializeField] private float groundCheckRadius = 0.4f;

    [SerializeField] private Transform insideTransform;
    private Transform groundCheck;    
     public LayerMask groundLayer; 

    private Coroutine jump;

    private Boolean notEnter;
    private bool usePlatformMechanics;
    private bool jumped;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<Movement>();
        groundCheck = player.transform.Find("groundCheck");

        pendulumStart = new Vector2(0, -1);
        platformRB = GetComponent<Rigidbody2D>();

        hingeJoint2D = GetComponent<HingeJoint2D>();

        leftPlatform = true;
        usePlatformMechanics = false;
        notEnter = false;
        jumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(leftPlatform){
            playerMovement.SetUsePlatformMechanics(true);
            if(!Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer)){
                player.transform.rotation = Quaternion.Euler(0, 0, 0);   
                playerRB.constraints = RigidbodyConstraints2D.FreezeRotation; 
                player.transform.parent = null;
                player.transform.localScale = new Vector3(1,1,1);
                //playerMovement.SetIsOnPlatfrom(false);
                //playerRB.bodyType = RigidbodyType2D.Dynamic;
                playerRB.simulated = true;
                usePlatformMechanics = false;
                playerMovement.SetUsePlatformMechanics(usePlatformMechanics);
                notEnter = false;
                leftPlatform = false;
                jumped = false;
            }
            
        }

        

        //platformJump();
        if(usePlatformMechanics && !jumped){
            platformMove();
            platformJump();
        }
        
    }

    private void platformMove()
    {

        float moveInput = Input.GetAxis("Horizontal"); 

        player.transform.position += player.transform.right * moveInput * Time.deltaTime * moveSpeed;
        
    }

    private void platformJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            notEnter = true;
            Vector2 playerPosOld = player.transform.position;
            player.transform.position += Vector3.up*0.2f;
            Vector2 playerPosNew = player.transform.position;

            playerRB.simulated = true;

            player.transform.parent = null;
            player.transform.localScale = new Vector3(1,1,1);

            playerMovement.SetUsePlatformMechanics(true);
            playerMovement.SetIsGrounded(false);

            playerRB.velocity = (playerPosNew - playerPosOld)*50;
            float limitedYVel = Mathf.Clamp(platformRB.velocity.y, 0, 100);
            playerRB.velocity = new Vector2(platformRB.velocity.x, playerRB.velocity.y + limitedYVel);
            jumped = true;
        }
    }

    private IEnumerator Jump()
    { 
        yield return new WaitForSeconds(2f);
        Debug.Log("Force Applied");
        playerRB.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")) //&& direction == Vector3.down
        {
            pendulumUp = transform.up;
            RotateToMatchPendulum(player.transform, pendulumUp);
            if(playerRB != null){
                
                
                if(!notEnter){
                    playerRB.simulated = false;
                    playerRB.constraints = RigidbodyConstraints2D.None;  
                }
                
            }
            leftPlatform = false;
            usePlatformMechanics = true;
            playerMovement.SetUsePlatformMechanics(usePlatformMechanics);
            player.transform.parent = insideTransform;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")) //&& direction == Vector3.down
        {
            //playerRB.bodyType = RigidbodyType2D.Kinematic;
            pendulumUp = transform.up;
            RotateToMatchPendulum(player.transform, pendulumUp);
             
            //rotateAroundPendulumOrigin(player.transform, pendulumUp);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")) //&& direction == Vector3.down
        {
               //playerRB.gravityScale = 1.1f;
               //player.transform.rotation = Quaternion.Euler(0, 0, 0);  
               playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
               leftPlatform = true;
               //player.transform.parent = null;
        }
    }


    void RotateToMatchPendulum(Transform playerTransform, Vector2 pendulumUp)
    {
        // Calculate the rotation that matches the platform's up direction
        float angle = Mathf.Atan2(pendulumUp.y, pendulumUp.x) * Mathf.Rad2Deg - 90f;
        playerTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void rotateAroundPendulumOrigin(Transform playerTransform, Vector2 pendulumUp)
    {
        Vector2 pendulumCurrent = new Vector2(transform.position.x, transform.position.y) - hingeJoint2D.connectedAnchor;

        // Calculate the rotation that matches the platform's up direction
        float angle = Vector2.Angle(pendulumStart, pendulumCurrent) + Mathf.Atan2(pendulumUp.y, pendulumUp.x) * Mathf.Rad2Deg - 90f;
        Debug.Log(angle);
        playerTransform.RotateAround(hingeJoint2D.connectedAnchor, Vector3.forward, angle);
    }
}
