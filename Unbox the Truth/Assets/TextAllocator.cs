using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAllocator : MonoBehaviour
{
    // List to store the children's Transforms
    private List<GameObject> textMeshProChildren = new List<GameObject>();
    [SerializeField] private List<GameObject> doorReference = new List<GameObject>();

    void Start()
    {
        
        AllocateChildren();

        DisplayNameOfDoors();
        //Vector2 ScreenSpace = Camera.main.WorldToScreenPoint(objecttranform.position);
    }

    private void DisplayNameOfDoors()
    {
        for (int i = 0; i < textMeshProChildren.Count; i++)
        {
            // Get the position of the reference GameObject
            Vector3 referencePosition = doorReference[i].transform.position;

            // Set the position of the corresponding TextMeshPro GameObject
            textMeshProChildren[i].transform.position = referencePosition;

            // Optionally, you could adjust the rotation or scale as needed:
            // textMeshProObjects[i].transform.rotation = referenceObjects[i].transform.rotation;
            // textMeshProObjects[i].transform.localScale = referenceObjects[i].transform.localScale;
        }
        //
        // int i = 0;
        //     foreach (GameObject child in textMeshProChildren)
        //     {
        //         if (doorReference[i] != null)
        //         {
        //             child.transform.localPosition = doorReference[i].transform.position;
        //             i++;
        //         }
        //         else
        //         {
        //             child.transform.localPosition = Vector3.zero;
        //         }
        //     }
    }

    // void AllocateChildren()
    // {
    //     // Clear the list in case it was previously populated
    //     childrenList.Clear();
    //
    //     // Loop through each child of the parent GameObject
    //     foreach (GameObject child in transform)
    //     {
    //         // Add each child to the list
    //         childrenList.Add(child);
    //     }
    //
    //     // Log the number of children for demonstration
    //     Debug.Log("Number of children: " + childrenList.Count);
    //
    //     // Optionally, log the names of all children
    //     foreach (GameObject child in childrenList)
    //     {
    //         Debug.Log("Child: " + child.name);
    //     }
    // }
    private void AllocateChildren()
    {
        // Clear the list in case the script is re-executed (e.g., in the Editor)
        textMeshProChildren.Clear();

        // Loop through all children of the current Canvas
        foreach (Transform child in transform)
        {
            // Check if the child has a TextMeshPro component
            if (child.TryGetComponent(out TextMeshProUGUI textMeshPro))
            {
                // If it does, add the child GameObject to the list
                textMeshProChildren.Add(child.gameObject);
            }
        }
        
        // Optionally, print the names of the GameObjects
        foreach (var go in textMeshProChildren)
        {
            Debug.Log("TextMeshPro GameObject: " + go.transform.localPosition);
        }
    }
}
