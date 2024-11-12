using UnityEngine;

public class PickUp : MonoBehaviour
{
    private float playerDirection = 1f;
    private Movement playerMovementComponent;
    private GameObject player;
    private float width = 0.1f;
    
    [SerializeField] private float range = 1.0f;

    void Start()
    {
        playerMovementComponent = GetComponent<Movement>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        BoxCollider2D playerCol = player.GetComponent<BoxCollider2D>();
        width = playerCol.size.x;

        
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && playerMovementComponent.isCarryingObject == false)
        {
            playerDirection = playerMovementComponent.getLastDirection()*1f;
            Vector3 direction = new Vector3(playerDirection, 0, 0);
            Vector3 rayOrig = new Vector3(player.transform.position.x + (width/2 + 0.01f)*playerDirection, player.transform.position.y, 0);
            Ray pickUpRay = new Ray(rayOrig, direction);
            
            
            Debug.DrawRay(pickUpRay.origin, pickUpRay.direction * range);
            
            
            RaycastHit2D HitInformation = Physics2D.Raycast(pickUpRay.origin, pickUpRay.direction, range);

            if (HitInformation.collider)
            {
                //gotta ignore a few stuff, level doors messes everything up
                IInteractibles interactibleObject = HitInformation.collider.GetComponent<IInteractibles>();
                if (interactibleObject != null )
                {
                    interactibleObject.Interact(gameObject);
                }
            }
            else
            {
                //Debug.Log("NOTHING was interacted with");
            }
            
        }
    }

}
