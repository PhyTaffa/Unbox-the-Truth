using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedFire : MonoBehaviour
{
    private Vector3 startScale;
    //private Vector3 scaleDelta;
    [Header("Timer settings")]
    [SerializeField] private bool isActive = true;
    [SerializeField] private float currTimer = 3f;
    [SerializeField] private float timeOfSwitch = 3f;
    //[SerializeField] private float scaleDeltaDuration = 2f;

    private void Start()
    {
        startScale = transform.localScale;
        
        //scaleDelta = new Vector3(0f, startScale.y / scaleDeltaDuration, 0f);
    }

    void Update()
    {
        currTimer += Time.deltaTime;

        if (currTimer >= timeOfSwitch)
        {
            if (isActive)
            {
                //next line shold become a smoothing, or animation will fix it :^)
                transform.localScale = Vector2.zero;
                //StartCoroutine(ChangeFireScale(Vector3.zero, scaleDelta));
                
                isActive = false;
                currTimer = 0f;
            }
            else if (!isActive)
            {
                //next line shold become a smoothing, or animation will fix it :^)
                transform.localScale = startScale;
                //StartCoroutine(ChangeFireScale(startScale, -scaleDelta));

                
                isActive = true;
                currTimer = 0f;
            }
        }

        
    }

    private IEnumerator ChangeFireScale(Vector3 targetToGiven, Vector3 scaleDeltaGiven)
    {
        Vector3 targetToReach = targetToGiven;
        while (transform.localScale != targetToReach)
        {
            transform.localScale += scaleDeltaGiven;
            //Vector3.MoveTowards(transform.localScale, targetToReach, Time.deltaTime / scaleDeltaDuration);
            //transform.localScale = Vector3.Lerp(transform.localScale, targetToReach, Time.deltaTime / scaleDeltaDuration);
            
            yield return null;
        }
    }
}

