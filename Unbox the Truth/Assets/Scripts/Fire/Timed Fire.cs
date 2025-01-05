using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class TimedFire : MonoBehaviour
{
    private Vector3 startScale;
    //private Vector3 scaleDelta;
    [Header("Timer settings")]
    [SerializeField] private bool isActive = true;
    [SerializeField] private float currTimer = 3f;
    [SerializeField] private float timeOfSwitch = 3f;
    [SerializeField] private float timeDelay = 0f;

    private Light2D Light;
    private Light2D SaveLight;
    private GameObject childGo;
    //[SerializeField] private float scaleDeltaDuration = 2f;

    private void Start()
    {
        startScale = transform.localScale;
        Light = GetComponentInChildren<Light2D>();
        SaveLight = Light;
        childGo = transform.GetChild(0).gameObject;
        
        //scaleDelta = new Vector3(0f, startScale.y / scaleDeltaDuration, 0f);
    }

    void Update()
    {
        currTimer += Time.deltaTime;

        if (currTimer >= (timeOfSwitch + timeDelay))
        {
            if (isActive)
            {
                //next line shold become a smoothing, or animation will fix it :^)
                transform.localScale = Vector2.zero;
                //StartCoroutine(ChangeFireScale(Vector3.zero, scaleDelta));
                
                isActive = false;
                currTimer = 0f;
                childGo.SetActive(false);
                
            }
            else if (!isActive)
            {
                //next line shold become a smoothing, or animation will fix it :^)
                transform.localScale = startScale;
                //StartCoroutine(ChangeFireScale(startScale, -scaleDelta));

                
                isActive = true;
                currTimer = 0f;
                
                childGo.SetActive(true);
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

