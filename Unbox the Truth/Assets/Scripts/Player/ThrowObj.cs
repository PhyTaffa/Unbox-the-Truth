using UnityEngine;

public class ThrowObj : MonoBehaviour
{
    [SerializeField] private float throwForceX = 5f;
    [SerializeField] private float throwForceY = 7f;
    private Rigidbody rb;
    
    private float playerDirection = 1f;
    private GameObject player;
    private Movement move; //used to check and change the isCarryingObj
    
    private playerSoundPlayer psp;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        move = GetComponent<Movement>();
        
        psp = GetComponent<playerSoundPlayer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && move.isCarryingObject == true)
        {
            playerDirection = move.getLastDirection()*1f;
            Vector3 throwDirection = new Vector3(playerDirection * throwForceX, throwForceY, 0);
            
            GameObject boxTag = FindChildWithTag("Box");

            if (boxTag != null)
            {
                psp.PlaySpecificSound(playerSoundPlayer.Action.Throw);
                
                IBox box = boxTag.GetComponent<IBox>();
                box.Interact(player, throwDirection);
            }
        }
    }
    
    private GameObject FindChildWithTag(string tag)
    {
        // Iterate through each child of the player
        foreach (Transform child in transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject; // Return the first child found with the tag
            }
        }
        return null; // Return null if no child with the tag is found
    }
    
}
