using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndThrow : MonoBehaviour
{
    
    float playerDirection = 1f;
    MoveTest move;
    GameObject player;
    
    [SerializeField] float distance = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<MoveTest>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("");
            /*
            Ray raycastRay = camera.ScreenPointToRay(
                new Vector3
                (
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    1.0f
                )
            );
            */

            BoxCollider2D playerCol = player.GetComponent<BoxCollider2D>();
            float width = playerCol.size.x;
            
            playerDirection = move.getLastDirection()*1f;
            Vector3 direction = new Vector3(playerDirection, 0, 0);
            Vector3 rayOrig = new Vector3(player.transform.position.x + (width/2 + 0.01f)*playerDirection, player.transform.position.y, 0);
            Ray pickUpRay = new Ray(rayOrig, direction);
            
            Debug.DrawRay(pickUpRay.origin, pickUpRay.direction * distance);
            
            
            RaycastHit2D HitInformation = Physics2D.Raycast(pickUpRay.origin, pickUpRay.direction, distance);

            if (HitInformation.collider)
            {
                IPressurePlateTrigger interactibleObject = HitInformation.collider.GetComponent<IPressurePlateTrigger>();
                if (interactibleObject != null)
                {
                    PickUp();
                }
            }
            else
            {
                Debug.Log("NOTHING was interacted with");
            }
            
        }
    }

    protected void PickUp()
    {
        Debug.Log("Box was interacted with");
    }
}
