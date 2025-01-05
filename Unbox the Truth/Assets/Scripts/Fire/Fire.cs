using System.Collections;
using UnityEngine;
public class Fire : MonoBehaviour, IInteractibles
{     
    private float active;
    private float inactive;

    public void Interact(GameObject instigator)
    {
        //Debug.Log("permanently off");
    }
    public void UnInteract(GameObject instigator)
    {
        
    }
}

