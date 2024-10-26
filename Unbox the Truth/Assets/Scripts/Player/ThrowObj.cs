using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObj : MonoBehaviour
{
        
    private float playerDirection = 1f;
    MoveTest move; //used to check the isCarryingObj
    private float width = 0.1f;

    IBox box;

    // Start is called before the first frame update
    void Start()
    {
        
        move = GetComponent<MoveTest>();
        
        //move.isCarryingObject = true;
        //player = GameObject.FindGameObjectWithTag("Player");

        //BoxCollider2D playerCol = player.GetComponent<BoxCollider2D>();
        //width = playerCol.size.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && move.isCarryingObject == true)
        {
            //thorw the obj: applies forces to it, -> changes the value of isCarrying obj and deparent it.
            playerDirection = move.getLastDirection()*1f;
      
            Vector3 direction = new Vector3(playerDirection, 0, 0);

            
        }
    }
}
