using UnityEngine;

public interface IInteractibles
{
    public abstract void Interact(GameObject instigator);
    public abstract void UnInteract(GameObject instigator);
}