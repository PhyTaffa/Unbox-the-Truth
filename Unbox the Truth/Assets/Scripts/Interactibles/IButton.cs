
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class IButton : MonoBehaviour, IInteractibles
{
    [FormerlySerializedAs("gameObject")] [SerializeField] private GameObject gameObjectToBeInteractedWith;
    private IInteractibles willInteractWith;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private GenericAudioPlayerLevels gapl;
    
    public void Start()
    {
        willInteractWith = gameObjectToBeInteractedWith.GetComponent<IInteractibles>();
        gapl = GetComponent<GenericAudioPlayerLevels>();
    }

public void Interact(GameObject Instigator)
    {  
        //instigator = player. use willInteractWith to do the funny
        if (willInteractWith != null)
        {
            gapl.DJPPPPlayThatShid();
            willInteractWith.Interact(Instigator);
        }
        else
        {
            //Debug.Log("Button binded to NOTHING.");
        }
    }

    public void UnInteract(GameObject Instigator)
    {
        //Debug.Log("Button un-iteracted with");
    }

}
