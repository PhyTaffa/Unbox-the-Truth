using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IBox : MonoBehaviour, IInteractibles
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(GameObject Instigator)
    {
        Debug.Log("Box was interacted with");
    }
}
