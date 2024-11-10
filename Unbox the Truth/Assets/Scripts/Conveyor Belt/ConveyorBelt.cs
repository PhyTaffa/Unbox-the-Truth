using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float rotationalSpeed = 5f;

    private float directionX;
    private float directionY;
    // Start is called before the first frame update
    void Start()
    {
        //X direction
        //direction = direction.x;
    
        //Y direction

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Transform collidedObjTransform = other.gameObject.GetComponent<Transform>();
        
        
        if (collidedObjTransform.position.y >= transform.position.y) //on top
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, 0f) * rotationalSpeed, ForceMode2D.Force);
        }
        else if (collidedObjTransform.position.y < transform.position.y)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-direction.x, 0f) * rotationalSpeed, ForceMode2D.Force);
        }
        else if (collidedObjTransform.position.x >= transform.position.x)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,-direction.y) * rotationalSpeed, ForceMode2D.Force);
        }
        else if (collidedObjTransform.position.x < transform.position.x)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,direction.y) * rotationalSpeed, ForceMode2D.Force);
        }
        
        
        
        
        //Debug.Log(other.gameObject.name);
        //Transform otherTransform = other.transform;
    }
}
