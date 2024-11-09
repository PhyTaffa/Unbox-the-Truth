using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IFire : MonoBehaviour, IInteractibles
{
    [SerializeField] private int secondsToDeactivate = 240;
    private Vector3 startScale;
    private Vector3 scaleChange;

    public void Start()
    {
        startScale = transform.localScale;
        scaleChange = new Vector3(0f, -startScale.y / secondsToDeactivate, 0f);
    }
    
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     
    //     if (other.CompareTag("Player"))
    //     {
    //         Debug.Log("died");
    //     }
    // }

    
    
    private IEnumerator FirePillarCycle()
    {
        while (transform.localScale.y > 0)
        {
            
            
            transform.localScale += scaleChange;

            yield return null;
        }
        transform.localScale = Vector3.zero;
    }
    
    public void Interact(GameObject instigator)
    {
        //transform.localScale = targetScale;
        StartCoroutine(FirePillarCycle());
        
        //Debug.Log("permanently off");
    }
    public void UnInteract(GameObject instigator)
    {
        
    }
}
