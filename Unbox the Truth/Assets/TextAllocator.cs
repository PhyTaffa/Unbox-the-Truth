using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAllocator : MonoBehaviour
{
    // List to store the children's Transforms
    private List<GameObject> childrenList = new List<GameObject>();

    void Start()
    {
        //AllocateChildren();
    }

    void AllocateChildren()
    {
        // Clear the list in case it was previously populated
        childrenList.Clear();

        // Loop through each child of the parent GameObject
        foreach (GameObject child in transform)
        {
            // Add each child to the list
            childrenList.Add(child);
        }

        // Log the number of children for demonstration
        Debug.Log("Number of children: " + childrenList.Count);

        // Optionally, log the names of all children
        foreach (GameObject child in childrenList)
        {
            Debug.Log("Child: " + child.name);
        }
    }
}
