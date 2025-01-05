using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private float playerDirection = 1f;
    private Movement playerMovementComponent;
    private GameObject player;
    private float width = 0.1f;
    
    [SerializeField] private float range = 1.0f;

    private PlayerSoundPlayer psp;
    void Start()
    {
        playerMovementComponent = GetComponent<Movement>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        BoxCollider2D playerCol = player.GetComponent<BoxCollider2D>();
        width = playerCol.size.x;

        psp = GetComponent<PlayerSoundPlayer>();
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            
            
            playerDirection = playerMovementComponent.getLastDirection()*1f;
            Vector3 direction = new Vector3(playerDirection, 0, 0);
            Vector3 rayOrig = new Vector3(player.transform.position.x + (width/2 + 0.01f)*playerDirection, player.transform.position.y, 0);
            Ray pickUpRay = new Ray(rayOrig, direction);
            
            
            Debug.DrawRay(pickUpRay.origin, pickUpRay.direction * range);
            
            
            RaycastHit2D[] HitInformation = Physics2D.RaycastAll(pickUpRay.origin, pickUpRay.direction, range);

            for (int i = 0; i < HitInformation.Length; i++)
            {
                if (HitInformation[i].collider)
              {
                  IInteractibles interactibleObject = HitInformation[i].collider.GetComponent<IInteractibles>();
                  if (interactibleObject != null)
                  {
                      if(HitInformation[i].collider.CompareTag("Box"))
                      {
                          //sounding
                          psp.PlaySpecificSound(PlayerSoundPlayer.Action.PickUp);
                          
                          interactibleObject.Interact(gameObject);
                      }
                      else if (HitInformation[i].collider.CompareTag("Interactable"))
                      {
                          interactibleObject.Interact(gameObject);
                      }

                      return;
                  }
                  else
                  {
                      //Debug.Log("NOTHING was interacted with");
                  }
              }
            }
            

            
        }
    }

}
