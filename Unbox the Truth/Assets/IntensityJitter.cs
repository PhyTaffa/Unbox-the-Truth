using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntensityJitter : MonoBehaviour
{
    Light2D Light;
    // Start is called before the first frame update
    void Start()
    {
        Light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Light.intensity = Random.Range(0.5f, 1.5f);
    }
}
