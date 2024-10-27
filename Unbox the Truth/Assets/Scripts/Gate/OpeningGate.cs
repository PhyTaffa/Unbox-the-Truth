using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningGate : MonoBehaviour
{
    [SerializeField] private GameObject endPointGameObject;
    [SerializeField] private float moveSpeed = 0.1f;
    private Vector3 endCoordinate;
    public void Interact()
    {
        endCoordinate = endPointGameObject.transform.position;
        
        if (transform.position != endCoordinate)
        {
            StartCoroutine(MoveToTarget());
        }
    }
    
    private IEnumerator MoveToTarget()
    {
        // Continue moving until reaching the target
        //should add a small buffer zone
        while (transform.position != endCoordinate)
        {
            // Dynamically updates the target position
            endCoordinate = endPointGameObject.transform.position;
            
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, endCoordinate, moveSpeed * Time.deltaTime);
            
            yield return null; // Wait until the next frame
        }

        // Ensure the object ends exactly at the target
        transform.position = endCoordinate;
    }
}
