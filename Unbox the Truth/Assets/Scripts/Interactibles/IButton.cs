
using Unity.VisualScripting;
using UnityEngine;

public class IButton : MonoBehaviour, IInteractibles
{
    [SerializeField] private GameObject willInteractWith;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public void Interact(GameObject Instigator)
    {
        //instigator = player. use willInteractWith to do the funny
        if (willInteractWith != null)
        {
            MoveTheDamnDoor(willInteractWith);
        }
        else
        {
            Debug.Log("Button bind to NOTHING.");
        }
    }

    private void MoveTheDamnDoor(GameObject door)
    {
        OpeningGate openingGate = door.GetComponent<OpeningGate>();
        if (openingGate != null)
        {
            openingGate.Interact();    
        }
        else
        {
            OpeningGateUsingChild openingGate2 = door.GetComponent<OpeningGateUsingChild>();
            openingGate2.Interact();
        }
        

        //GameObject endPosition = door.GetComponent<GameObject>();
    }
}
