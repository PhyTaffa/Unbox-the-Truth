using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;  // The SpriteRenderer component
    public KeyCode triggerKey = KeyCode.B; // The key to start the rainbow effect

    private bool isRainbowActive = false; // To track if the rainbow effect is active
    private float colorChangeSpeed = 1f;  // Speed at which the sprite color changes
    private float hue = 0f;               // Hue for rainbow effect (0 to 1)

    private void Start()
    {
        // Fetch the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing!");
        }
    }

    private void Update()
    {
        // Check if the trigger key (N) is pressed
        if (Input.GetKeyDown(triggerKey) && !isRainbowActive)
        {
            isRainbowActive = true;
            StartCoroutine(ChangeSpriteColor());
        }
        else if (Input.GetKeyDown(triggerKey) && isRainbowActive)
        {
            isRainbowActive = false;
            StopCoroutine(ChangeSpriteColor());
            spriteRenderer.color = Color.white; // Reset color to white when the effect stops
        }
    }

    // Coroutine to smoothly cycle through colors
    IEnumerator ChangeSpriteColor()
    {
        while (isRainbowActive)
        {
            // Update the hue value over time
            hue += Time.deltaTime * colorChangeSpeed;
            if (hue > 1f) hue = 0f; // Reset hue to cycle back to red

            // Convert the hue to RGB and set it to the sprite color
            spriteRenderer.color = Color.HSVToRGB(hue, 1f, 1f);

            // Wait until the next frame
            yield return null;
        }
    }
}
