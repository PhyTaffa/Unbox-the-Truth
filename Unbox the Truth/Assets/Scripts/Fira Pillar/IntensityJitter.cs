using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IntensityJitter : MonoBehaviour
{
    Light2D Light;
    private float minIntensity = 1f;
    private float maxIntensity = 4f;
    private float jitterSpeed = 0.4f;
    private float targetIntensity;

    // Start is called before the first frame update
    void Start()
    {
        Light = GetComponent<Light2D>();
        targetIntensity = Random.Range(minIntensity, maxIntensity);
    }

    // Update is called once per frame
    void Update()
    {
        // Smoothly transition to the target intensity
        Light.intensity = Mathf.Lerp(Light.intensity, targetIntensity, jitterSpeed);

        // If close to the target, choose a new random target intensity
        if (Mathf.Abs(Light.intensity - targetIntensity) < 0.01f)
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}