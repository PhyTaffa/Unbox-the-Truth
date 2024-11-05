using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedFire : MonoBehaviour
{
        private float activeTime = 2.0f;     
        private float inactiveTime = 2.0f;   
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
                yield return new WaitForSeconds(activeTime);

                
              
                isActive = false;
                yield return new WaitForSeconds(inactiveTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
           
            if (isActive && other.CompareTag("Player"))
            {
                
                Debug.Log("Player takes damage!");
                
            }
        }
}

