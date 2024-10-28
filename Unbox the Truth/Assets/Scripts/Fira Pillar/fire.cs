using System.Collections;
using UnityEngine;
public class fire : MonoBehaviour
{     
    float active;
    float inactive;
    private bool isActive = false;
    private void Start()
    {
        StartCoroutine(FirePillarCycle());
    }
    private IEnumerator FirePillarCycle()
    {
        while (true)
        {
            isActive = true;
       
            yield return new WaitForSeconds(active);
            
            isActive = false;
            yield return new WaitForSeconds(inactive);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (isActive && other.CompareTag("Player"))
        {
            Debug.Log("died");
        }
    }
}

