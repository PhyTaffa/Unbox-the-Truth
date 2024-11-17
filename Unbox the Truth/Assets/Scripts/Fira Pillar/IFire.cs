using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IFire : MonoBehaviour, IInteractibles
{
    [SerializeField] private int secondsToDeactivate = 240;
    private Vector3 startScale;
    private Vector3 scaleChange;
    private bool hasInteracted = false;

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
       if(hasInteracted) return;
        StartCoroutine(FirePillarCycle());
        Light2D Light = GetComponentInChildren<Light2D>();
        Light.intensity = 0;

        Destroy(Light);
        hasInteracted = true;
        
        //Debug.Log("permanently off");
    }
    public void UnInteract(GameObject instigator)
    {
        
    }
}
