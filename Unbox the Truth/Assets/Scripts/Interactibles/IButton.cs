
using Unity.VisualScripting;
using UnityEngine;

public class IButton : MonoBehaviour, IInteractibles
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(GameObject Instigator, GameObject instigatedObject)
    {
        Debug.Log("Button was interacted with");
    }
}
