using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningGateUsingChild : MonoBehaviour, IInteractibles
{
    private GameObject endPointGameObject;
    private Transform sibling;
    
    [SerializeField] private float moveSpeed = 0.1f;
    
    private Vector3 endCoordinate;
    void Start()
    {
        sibling = FindSiblingByTag("Target");

        endPointGameObject = sibling.gameObject;
        
    }
    private Transform FindSiblingByTag(string tag)
    {
        // Get the parent of this GameObject
        Transform parent = transform.parent;

        if (parent != null)
        {
            // Access the sibling directly based on the child count
            // Assuming only two children exist
            int siblingIndex = transform.GetSiblingIndex();
            int siblingCount = parent.childCount;

            // Get the other sibling directly based on the index
            Transform potentialSibling = parent.GetChild(1 - siblingIndex);

            // Check if the found sibling has the correct tag
            if (potentialSibling.CompareTag(tag))
            {
                return potentialSibling; // Return the sibling if the tag matches
            }
        }

        return null; // Return null if no sibling found
    }
    public void Interact(GameObject Instigator)
    {
        endCoordinate = endPointGameObject.transform.position;
        if (transform.position != endCoordinate)
        {
            StartCoroutine(MoveToTarget());
        }
    }

    public void UnInteract(GameObject Instigator)
    {
        // Do nothing
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
