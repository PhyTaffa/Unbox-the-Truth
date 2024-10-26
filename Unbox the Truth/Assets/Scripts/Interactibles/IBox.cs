using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class IBox : MonoBehaviour, IInteractibles
{
    //private IBox iBox;
    [SerializeField] private bool isBeingCarried = false;
    //Start is called before the first frame update
    void Start()
    {
        //iBox = gameObject.GetComponent<IBox>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(GameObject instigator)
    {   
        Debug.Log("Box was interacted with");
        
        MoveTest playerProperty = instigator.GetComponent<MoveTest>();

        
        playerProperty.isCarryingObject = true;
        //playerProperty.moveSpeed = 0.06f;
        
        GettingPickedUp(instigator);
        Parenting(instigator);
    }

    private void Parenting(GameObject player)
    {
        transform.parent = player.transform;
    }

    private void GettingPickedUp(GameObject player)
    {
        //Current position of player
        Vector3 currentPosition = player.transform.position;
        
        //Size pf the box
        BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
        float boxHeight = boxCol.size.y;
        
        //Calculating the appropriate position
        Vector3 pickUpPosition = player.transform.position + new Vector3(0, boxHeight, 0);

        // To be Changed with more smooth movement
        transform.position = pickUpPosition;
    }

    private void GettingThrown()
    {
        
        isBeingCarried = false;
    }
}
