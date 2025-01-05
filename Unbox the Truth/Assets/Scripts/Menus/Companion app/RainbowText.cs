using System.Collections;
using UnityEngine;
using TMPro;

public class RainbowText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;  // The TextMeshPro component
    public KeyCode triggerKey = KeyCode.N;  // The key to start the rainbow effect

    private bool isRainbowActive = false; // To track if the rainbow effect is active
    private float colorChangeSpeed = 1f;  // Speed at which the text color changes
    private float hue = 0f;               // Hue for rainbow effect (0 to 1)

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Check if the trigger key (N) is pressed
        if (Input.GetKeyDown(triggerKey) && !isRainbowActive)
        {
            isRainbowActive = true;
            StartCoroutine(ChangeTextColor());
        }
        else if (Input.GetKeyDown(triggerKey) && isRainbowActive)
        {
            isRainbowActive = false;
            StopCoroutine(ChangeTextColor());
            textMeshPro.color = Color.white; // Reset color to white when the effect stops
        }
    }

    // Coroutine to smoothly cycle through colors
    IEnumerator ChangeTextColor()
    {
        while (isRainbowActive)
        {
            // Update the hue value over time
            hue += Time.deltaTime * colorChangeSpeed;
            if (hue > 1f) hue = 0f; // Reset hue to cycle back to red

            // Convert the hue to RGB and set it to the text color
            textMeshPro.color = Color.HSVToRGB(hue, 1f, 1f);

            // Wait until the next frame
            yield return null;
        }
    }
}