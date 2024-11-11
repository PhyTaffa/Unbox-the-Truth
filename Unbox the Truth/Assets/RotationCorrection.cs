using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCorrection : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D playerRB;

    private bool leftPlatform;
    private Vector2 pendulumUp;
    [SerializeField] private float groundCheckRadius = 0.4f;
    private Transform groundCheck;    
     public LayerMask groundLayer; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        groundCheck = player.transform.Find("groundCheck");

        leftPlatform = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(leftPlatform){
            if(!Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer)){
                player.transform.rotation = Quaternion.Euler(0, 0, 0);   
                playerRB.constraints = RigidbodyConstraints2D.FreezeRotation; 
                //player.transform.parent = null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")) //&& direction == Vector3.down
        {
            //collision.transform.parent = transform;
            playerRB.constraints = RigidbodyConstraints2D.None;  
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")) //&& direction == Vector3.down
        {
               
                pendulumUp = transform.up;
                RotateToMatchPendulum(player.transform, pendulumUp); 
                //playerRB.gravityScale = 0;   
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")) //&& direction == Vector3.down
        {
               //playerRB.gravityScale = 1;
               //collision.transform.parent = null;

               //player.transform.rotation = Quaternion.Euler(0, 0, 0);   
               playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
               leftPlatform = true;
        }
    }


    void RotateToMatchPendulum(Transform playerTransform, Vector2 pendulumUp)
    {
        // Calculate the rotation that matches the platform's up direction
        float angle = Mathf.Atan2(pendulumUp.y, pendulumUp.x) * Mathf.Rad2Deg - 90f;
        playerTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
