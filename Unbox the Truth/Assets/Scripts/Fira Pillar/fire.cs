using System.Collections;
using UnityEngine;
public class fire : MonoBehaviour, IInteractibles
{     
    private float active;
    private float inactive;
    //private bool isActive;
    private void Start()
    {
        //isActive = false;
        //StartCoroutine(FirePillarCycle());
    }
    
    
    // private IEnumerator FirePillarCycle()
    // {
    //     while (true)
    //     {
    //         isActive = true;
    //    
    //         yield return new WaitForSeconds(active);
    //         
    //         isActive = false;
    //         yield return new WaitForSeconds(inactive);
    //     }
    // }
    
    
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     
    //     if (isActive && other.CompareTag("Player"))
    //     {
    //         Debug.Log("died");
    //     }
    // }

    public void Interact(GameObject instigator)
    {
        //Debug.Log("permanently off");
    }
    public void UnInteract(GameObject instigator)
    {
        
    }
}

