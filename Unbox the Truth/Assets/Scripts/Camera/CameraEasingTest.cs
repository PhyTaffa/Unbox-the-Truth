using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEasingTest : MonoBehaviour
{
    [Header("FOV Settings")]
    [SerializeField] private float currFOV = 80f;
    [SerializeField] private float maxFOV = 120f;
    [SerializeField] private float minFOV = 80f;
    [SerializeField] private float duration = 1.5f; // Duration for the transition

    private Camera cameraComponent;
    private float targetFOV;
    private float transitionTime = 0f; // Tracks the time for transition
    private bool isKeyPressed = false; // Tracks whether "B" key is pressed
    private bool isTransitioning = false; // Flag to control if we are in a transition
    private float initialFOV; // The FOV value when starting the transition

    private void Awake()
    {
        // Assume this script is attached to the Camera, so we can directly reference the Camera component
        cameraComponent = GetComponent<Camera>();
        targetFOV = currFOV;  // Initialize target FOV to currFOV
        initialFOV = currFOV; // Initialize the initial FOV for reversals
    }

    private void Update()
    {
        // Check if the B key is being pressed
        bool keyPressed = Input.GetKey(KeyCode.B);

        // If the key state has changed (pressed or released), reset transition time or reverse the easing
        if (keyPressed != isKeyPressed)
        {
            isKeyPressed = keyPressed;

            if (keyPressed)
            {
                // If key is pressed, start transitioning to maxFOV
                initialFOV = currFOV; // Remember the current FOV when starting the transition
                transitionTime = 0f;   // Reset transition time to start the easing
                isTransitioning = true; // Start the easing
            }
            else
            {
                // If key is released, reverse the transition back to minFOV
                isTransitioning = true; // Continue easing but in reverse direction
            }
        }

        // If transitioning, calculate easing
        if (isTransitioning)
        {
            // Increment transition time
            transitionTime += Time.deltaTime;

            // Clamp transitionTime to the duration to prevent it from exceeding the duration
            if (transitionTime > duration)
                transitionTime = duration;

            // Calculate normalized time value for easing (0 to 1)
            float t = transitionTime / duration;

            // Apply EaseInOutQuint to the normalized time value
            float easedT = EaseInOutQuint(t);

            // If we are reversing the transition (key released), ease back towards minFOV
            if (!keyPressed)
            {
                easedT = 1f - easedT; // Reverse the easing
            }

            // Smoothly interpolate between the initial FOV (or current FOV) and the target FOV using easedT
            currFOV = Mathf.Lerp(initialFOV, keyPressed ? maxFOV : minFOV, easedT);

            // Apply the current FOV to the camera's field of view
            cameraComponent.fieldOfView = currFOV;

            // If we are finished transitioning, stop the easing
            if (transitionTime >= duration)
            {
                isTransitioning = false;
            }
        }
        else
        {
            // Apply target FOV directly when not transitioning
            cameraComponent.fieldOfView = currFOV;
        }
    }

    // EaseInOutQuint function for smooth easing (ease-in-out using quintic function)
    private float EaseInOutQuint(float t)
    {
        return t < 0.5f ? 16f * t * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 5f) / 2f;
    }
}
