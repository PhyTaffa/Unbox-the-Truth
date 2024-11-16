using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IPlayer : MonoBehaviour, IPressurePlateTrigger
{
    private Movement move;
    private GameObject player;
    private GameObject worldRoot;
    private bool canRotateWorld;

    private int worldRotCount = 0;


    
    //[SerializeField] float distance = 1.5f;

    private Transform playerT;
    private Vector3 rotate;
    
    //Companion app Sprite manager
    private SpriteRenderer playerSprite;
    [SerializeField] private Sprite givenSprite;

    void Start()
    {
        move = GetComponent<Movement>();
        player = GameObject.FindGameObjectWithTag("Player"); 
        worldRoot = GameObject.FindGameObjectWithTag("WorldRoot"); 
        canRotateWorld = true;
        
        
        
        playerSprite = GetComponent<SpriteRenderer>();
        
        // Check if a sprite has been set in the SpriteManager
        // Check if spriteRenderer is assigned
        // if (playerSprite == null)
        // {
        //     Debug.LogError("SpriteRenderer has not been assigned!");
        //     return;
        // }

        
        ApplySelectedSprite();
        // // Apply the selected sprite if it's set in SpriteManager
        // if (SpriteManager.selectedSprite != null)
        // {
        //     Debug.Log("Applying sprite: " + SpriteManager.selectedSprite.name);
        //     spriteRenderer.sprite = SpriteManager.selectedSprite;
        // }
        // else
        // {
        //     Debug.LogWarning("No sprite selected in SpriteManager.");
        // }
    }
    
    private void ApplySelectedSprite()
    {
        // Check if spriteRenderer and SpriteManager are valid
        // if (playerSprite == null)
        // {
        //     Debug.LogError("SpriteRenderer has not been assigned!");
        //     return;
        // }

        if (SpriteManager.selectedSprite != null)
        {
            playerSprite.sprite = SpriteManager.selectedSprite;
            Debug.Log("Applied sprite: " + playerSprite.sprite.name);
        }
        else
        {
            Debug.LogWarning("No sprite selected in SpriteManager.");
        }
    }
    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     ApplySelectedSprite();
        // }
        // if (Input.GetKey(KeyCode.X))
        // {
        //     Debug.Log($"Key pressed, now sprite chaning to {givenSprite.name}");
        //     playerSprite.sprite = givenSprite;
        // }

        
    }

    /*
    private bool CanRotateWorld()
    {
        
        if(IsGrounded())
        {
            worldRotCount = 0;
            if(!move.isCarryingObject){
                return true;
            } else {
                return false;
            }
        }
        else
        {
            if(worldRotCount == 0)
            {
                worldRotCount = 1;
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }

    private bool IsGrounded()
    {
        return move.IsGrounded();
    }

    */
}
