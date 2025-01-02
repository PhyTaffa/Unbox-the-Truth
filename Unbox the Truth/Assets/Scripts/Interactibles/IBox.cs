using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class IBox : MonoBehaviour, IInteractibles
{
    //private IBox iBox;
    [SerializeField] private bool isBeingCarried = false;
    private WorldRotation worldRotation;
    private Rigidbody2D boxRB;

    private BoxCollider2D playerCollider;

    private Transform playerTransform;
    
    private float boxHeight;
    
    void Start()
    {
        //iBox = gameObject.GetComponent<IBox>();
        worldRotation = transform.parent.GetComponent<WorldRotation>();
        
        //here due to small usage of boxes, most likely it's better to let it exist just in the interact fundtion
        BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
        boxHeight = boxCol.size.y;
        
        boxRB = GetComponent<Rigidbody2D>();

        playerTransform = GameObject.FindWithTag("Player").transform;
        playerCollider = GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>();
    }

    public void Interact(GameObject instigator)
    {   
        Movement playerProperty = instigator.GetComponent<Movement>();

        playerProperty.isCarryingObject = true;
        
        GettingPickedUp(instigator);
        Parenting(instigator);
        UpdatePlayerCollider();
    }

    private void UpdatePlayerCollider()
    {
        Vector3 playerColliderSize = new Vector2(playerCollider.size.x, playerCollider.size.y + boxHeight);
        playerCollider.size = playerColliderSize;
        playerCollider.offset = new Vector2(playerCollider.offset.x, playerCollider.offset.y + boxHeight/2);
    }

    private void ResetPlayerCollider()
    {
        Vector3 playerColliderSize = new Vector2(playerCollider.size.x, playerCollider.size.y - boxHeight);
        playerCollider.size = playerColliderSize;
        playerCollider.offset = new Vector2(playerCollider.offset.x, playerCollider.offset.y - boxHeight/2);
    }

    //mystical polymorphism
    public void Interact(GameObject instigator, Vector3 trajectory)
    {
        //Debug.Log("Box was thrown");
        Movement playerProperty = instigator.GetComponent<Movement>();
        
        playerProperty.isCarryingObject = false;
        ResetPlayerCollider();
        DeParenting();
        GettingThrown(trajectory);
    }


    private void Parenting(GameObject player)
    {
        transform.parent = player.transform;
        boxRB.simulated = false;
    }

    private void GettingPickedUp(GameObject player)
    {
        //Calculating the appropriate position
        Vector3 pickUpPosition = player.transform.position + new Vector3(0, (playerTransform.localScale.y + boxHeight)/2, 0);

        //velocity of box 0
        boxRB.velocity = Vector2.zero;
        
        // To be Changed with more smooth movement Bézier curve
        transform.position = pickUpPosition;
        isBeingCarried = true;
    }

    private void DeParenting()
    {
        transform.parent = worldRotation.transform;
        boxRB.simulated = true;
    }

    public void GettingThrown(Vector3 thrownDirection)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        rb.velocity = thrownDirection;
        
        isBeingCarried = false;
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        //When a box touches the ground, the player stops carrying it
        
        //Debug.Log("Collision with " + col.gameObject.name);
        if (col.gameObject.layer == 6 && isBeingCarried)
        {
            //changes the value of isCarryingObj
            isBeingCarried = false;
            DeParenting();
            
            GameObject player = GameObject.FindWithTag("Player");
            Movement playerProperty = player.GetComponent<Movement>();
            playerProperty.isCarryingObject = false;
        }
    }

    public void UnInteract(GameObject instigator)
    {
        // do nothing
    }
}
