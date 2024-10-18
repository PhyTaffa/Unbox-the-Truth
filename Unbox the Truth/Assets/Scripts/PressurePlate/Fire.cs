using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, IPressurePlateInteractable
{
    public void interact()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}

