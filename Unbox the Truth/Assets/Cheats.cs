using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] internal bool isKillable = true;
    [SerializeField] internal bool canSpamRotation = false;
    [SerializeField] internal bool isCheating = false;
    [SerializeField] private float moveSpeed = 10f;
    private Rigidbody2D rb2d;
    private Movement moveComp;
    private BoxCollider2D box2d;
    
    private float originalGravityScale;

    void Start()
    {

        moveComp = GetComponent<Movement>();
        
        rb2d = GetComponent<Rigidbody2D>();
        originalGravityScale = rb2d.gravityScale;
        
        box2d = GetComponent<BoxCollider2D>();
                                                   
    }

    // Update is called once per frame
    void Update()
    {
        CheatMove();
        ChangeCheats();

        DisableCollision();

        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     rb2d.gravityScale = 0f;
        //     Debug.Log(rb2d.gravityScale);
        // }
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     rb2d.gravityScale = originalGravityScale;
        //     Debug.Log(rb2d.gravityScale);
        // }
    }

    private void DisableCollision()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            box2d.enabled = !box2d.enabled;
        }
    }

    private void ChangeCheats()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isCheating = !isCheating;

            isKillable = !isKillable;
            
            canSpamRotation = !canSpamRotation;

            if (isCheating)
            {
                rb2d.gravityScale = 0f;
            }
            else
            {
                rb2d.gravityScale = originalGravityScale;
            }
        }

    }

    private void CheatMove()
    {
        if (isCheating)
        {
            Vector2 movement = Vector2.zero;

            if (Input.GetKey(KeyCode.J)) // Left
            {
                movement.x = -1;
            }
            if (Input.GetKey(KeyCode.I)) // Up
            {
                movement.y = 1;
            }
            if (Input.GetKey(KeyCode.L)) // Right
            {
                movement.x = 1;
            }
            if (Input.GetKey(KeyCode.K)) // Down
            {
                movement.y = -1;
            }

            // Normalize the movement vector to ensure consistent speed
            rb2d.velocity = movement.normalized * moveSpeed;
        }
    }
}
