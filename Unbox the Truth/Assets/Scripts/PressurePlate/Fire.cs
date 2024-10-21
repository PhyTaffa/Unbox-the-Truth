using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, IPressurePlateInteractable
{
    public void interact()
    {
        Debug.Log("Fire interanct, implement correct behavior here");
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void unInteract()
    {   
        Debug.Log("Fire unInteract, implement correct behavior here");
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}

