
using Unity.VisualScripting;
using UnityEngine;

public class IButton : MonoBehaviour, IInteractibles
{
    public void Interact(GameObject Instigator)
    {
        Debug.Log("Button was interacted with");
    }
}
