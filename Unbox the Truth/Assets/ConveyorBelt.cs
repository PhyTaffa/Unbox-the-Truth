using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float rotationalSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Transform t = other.gameObject.GetComponent<Transform>();
        
        
        if (t.position.y > transform.position.y) //on top
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * rotationalSpeed);
        }
        else
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(-direction * rotationalSpeed);
        }
        
        
        
        
        //Debug.Log(other.gameObject.name);
        //Transform otherTransform = other.transform;
    }
}
