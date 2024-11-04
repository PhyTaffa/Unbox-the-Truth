
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class IButton : MonoBehaviour, IInteractibles
{
    [FormerlySerializedAs("gameObject")] [SerializeField] private GameObject gameObjectToBeInteractedWith;
    private IInteractibles willInteractWith;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        willInteractWith = gameObjectToBeInteractedWith.GetComponent<IInteractibles>();
    }

public void Interact(GameObject Instigator)
    {  
        //instigator = player. use willInteractWith to do the funny
        if (willInteractWith != null)
        {
            willInteractWith.Interact(Instigator);
            //MoveTheDamnDoor(willInteractWith);
        }
        else
        {
            Debug.Log("Button binded to NOTHING.");
        }
    }

    public void UnInteract(GameObject Instigator)
    {
        Debug.Log("Button un-iteracted with");
    }
    
    // private void MoveTheDamnDoor(GameObject door)
    // {
    //     OpeningGate openingGate = door.GetComponent<OpeningGate>();
    //     if (openingGate != null)
    //     {
    //         openingGate.Interact();    
    //     }
    //     else
    //     {
    //         OpeningGateUsingChild openingGate2 = door.GetComponent<OpeningGateUsingChild>();
    //         openingGate2.Interact();
    //     }
    //     
    //
    //     //GameObject endPosition = door.GetComponent<GameObject>();
    // }
}
