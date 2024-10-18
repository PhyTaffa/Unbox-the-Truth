using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IPressurePlateInteractable
{
    public void interact()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
