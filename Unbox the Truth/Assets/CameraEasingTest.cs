using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEasingTest : MonoBehaviour
{
[Header("FOV Settings")]
    [SerializeField] private float currFOV = 60f;
    [SerializeField] private float maxFOV = 90f;
    [SerializeField] private float minFOV = 60f;
    [SerializeField] private float duration = 1f; // Duration for the transition

    private Camera cameraComponent;
    private float targetFOV;
    private float transitionTime = 0f; // Tracks the time for transition
    private bool isKeyPressed = false; // Keeps track of whether B key is pressed

    private void Awake()
    {
        // Assume this script is attached to the Camera, so we can directly reference the Camera component
        cameraComponent = GetComponent<Camera>();
        targetFOV = currFOV;  // Initialize target FOV to currFOV
    }

    private void Update()
    {
        // Check if the B key is being pressed
        bool keyPressed = Input.GetKey(KeyCode.B);

        // If the key state has changed (pressed or released), reset transition time
        if (keyPressed != isKeyPressed)
        {
            isKeyPressed = keyPressed;
            transitionTime = 0f; // Reset transition time whenever key state changes
        }

        // Update the target FOV based on key press state
        if (keyPressed)
        {
            targetFOV = maxFOV; // When B is pressed, set target to maxFOV
        }
        else
        {
            targetFOV = minFOV; // When B is not pressed, set target to minFOV
        }

        // Accumulate transition time while the key is pressed or released
        transitionTime += Time.deltaTime;

        // Clamp transitionTime to the duration to prevent it from exceeding the duration
        if (transitionTime > duration)
            transitionTime = duration;

        // Calculate normalized time value for easing (0 to 1)
        float t = transitionTime / duration;

        // Apply EaseInOutQuint to the normalized time value
        float easedT = EaseInOutQuint(t);

        // Smoothly interpolate between current FOV and target FOV using eased time
        currFOV = Mathf.Lerp(currFOV, targetFOV, easedT);

        // Apply the current FOV to the camera's field of view
        cameraComponent.fieldOfView = currFOV;
    }

    // EaseInOutQuint function for smooth easing (ease-in-out using quintic function)
    private float EaseInOutQuint(float t)
    {
        return t < 0.5f ? 16f * t * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 5f) / 2f;
    }
}
