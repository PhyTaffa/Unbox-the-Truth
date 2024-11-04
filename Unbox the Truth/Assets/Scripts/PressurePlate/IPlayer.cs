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
    
    [SerializeField] float distance = 1.5f;

    private Transform playerT;
    private Vector3 rotate;

    void Start()
    {
        move = GetComponent<Movement>();
        player = GameObject.FindGameObjectWithTag("Player"); 
        worldRoot = GameObject.FindGameObjectWithTag("WorldRoot"); 
        canRotateWorld = true;
        
        //test(icle)
        playerT = player.transform;
        rotate = new Vector3(0, 0, 1);
    }
    

    // Update is called once per frame
    void Update()
    {
        //playerT.Rotate(rotate, 2);
        
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
