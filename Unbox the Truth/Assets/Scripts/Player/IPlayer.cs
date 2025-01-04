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

        // if (SpriteManagerSingleton.Instance == null)
        // {
        //     Instantiate(SpriteManagerSingleton);
        // }
        
        ApplySelectedSprite();
    }
    
    private void ApplySelectedSprite()
    {
        if (SpriteManagerSingleton.Instance.SelectedSprite != null)
        {
            playerSprite.sprite = SpriteManagerSingleton.Instance.SelectedSprite;
            //Debug.Log("Applied sprite: " + playerSprite.sprite.name);
        }
        else
        {
            //Debug.LogWarning("No sprite selected in SpriteManager.");
        }
    }

}
