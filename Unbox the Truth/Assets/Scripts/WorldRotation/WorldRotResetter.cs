using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotResetter : MonoBehaviour
{
    // Start is called before the first frame update

    private WorldRotation  worldRotation;
    private Movement playerMovement;
    void Start()
    {
        GameObject worldRotationGO = GameObject.FindWithTag("WorldRoot");
        worldRotation = worldRotationGO.GetComponent<WorldRotation>();

        GameObject playerGO = GameObject.FindWithTag("Player");
        playerMovement = playerGO.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject) //&& direction == Vector3.down
        {
            if(playerMovement.IsGrounded()){
                worldRotation.SetCanRotate(true);
            }
                
                //collision.transform.parent = transform;
                
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject) //&& direction == Vector3.down
        {
            if(playerMovement.IsGrounded()){
                worldRotation.SetCanRotate(true);
            }
                
        }
    }
}
