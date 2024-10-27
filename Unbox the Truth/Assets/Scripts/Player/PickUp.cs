using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    
    private float playerDirection = 1f;
    MoveTest move;
    GameObject player;
    private float width = 0.1f;
    
    [SerializeField] float distance = 1.5f;

    void Start()
    {
        move = GetComponent<MoveTest>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        BoxCollider2D playerCol = player.GetComponent<BoxCollider2D>();
        width = playerCol.size.x;

        
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && move.isCarryingObject == false)
        {
            playerDirection = move.getLastDirection()*1f;
            Vector3 direction = new Vector3(playerDirection, 0, 0);
            Vector3 rayOrig = new Vector3(player.transform.position.x + (width/2 + 0.01f)*playerDirection, player.transform.position.y, 0);
            Ray pickUpRay = new Ray(rayOrig, direction);
            
            
            Debug.DrawRay(pickUpRay.origin, pickUpRay.direction * distance);
            
            
            RaycastHit2D HitInformation = Physics2D.Raycast(pickUpRay.origin, pickUpRay.direction, distance);

            if (HitInformation.collider)
            {
                IInteractibles interactibleObject = HitInformation.collider.GetComponent<IInteractibles>();
                if (interactibleObject != null)
                {
                    interactibleObject.Interact(gameObject);
                }
            }
            else
            {
                Debug.Log("NOTHING was interacted with");
            }
            
        }
    }

}
