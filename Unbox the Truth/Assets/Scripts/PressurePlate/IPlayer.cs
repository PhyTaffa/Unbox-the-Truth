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

    void Start()
    {
        move = GetComponent<Movement>();
        player = GameObject.FindGameObjectWithTag("Player"); 
        worldRoot = GameObject.FindGameObjectWithTag("WorldRoot"); 
        canRotateWorld = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        //canRotateWorld = CanRotateWorld();
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
