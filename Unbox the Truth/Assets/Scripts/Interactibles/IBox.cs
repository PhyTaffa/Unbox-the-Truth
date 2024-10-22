using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class IBox : MonoBehaviour, IInteractibles
{
    private IBox iBox;
    // Start is called before the first frame update
    void Start()
    {
        iBox = gameObject.GetComponent<IBox>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(GameObject instigator, GameObject instigatedObject)
    {   //instigator is the player, so things can happen to him
        
        //the pick up stuff should happen here
        Debug.Log("Box was interacted with");
        
        MoveTest p = instigator.GetComponent<MoveTest>();

        p.isCarryingObject = true;
        
        SerializedObject serializedObject = new SerializedObject(p);
        //not being detected
        SerializedProperty playerCarryingObjProperty = serializedObject.FindProperty("isCarryingObject");
        
        //playerCarryingObjProperty.boolValue = true;
        
        GettingPickedUp(instigator, iBox);
    }

    private void GettingPickedUp(GameObject player, IBox pickedUpObject)
    {
        //Current position of player
        Vector3 currentPosition = player.transform.position;
        
        //Size pf the box
        BoxCollider2D boxCol = iBox.GetComponent<BoxCollider2D>();
        float boxHeight = boxCol.size.y;
        
        //Calculating the appropriate position
        Vector3 pickUpPosition = player.transform.position + new Vector3(0, boxHeight, 0);

        // To be Changed with more smooth movement
        transform.position = pickUpPosition;
    }
}
