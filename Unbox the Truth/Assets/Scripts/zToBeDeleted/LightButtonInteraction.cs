using UnityEngine;
using UnityEngine.UI;

public class LightButtonInteraction : MonoBehaviour
{
    public Light spotlight;  // The light object in the scene (can be a Point or Spot Light)
    public Button[] buttons;  // Array of buttons to interact with the light

    public Color lightColor = Color.white;  // Color of the button when the light is close
    public Color defaultColor = Color.gray;  // Default color of the button

    public float maxDistance = 10f;  // Max distance at which the light effect is applied

    void Update()
    {
        // Loop through all buttons
        foreach (Button button in buttons)
        {
            // Get the button's Image component
            Image buttonImage = button.GetComponent<Image>();
            if (buttonImage == null) continue;

            // Calculate the distance from the light to the button's position
            float distance = Vector3.Distance(spotlight.transform.position, button.transform.position);

            // Check if the button is within the light's effective range
            if (distance < maxDistance)
            {
                // Calculate the color intensity based on distance (closer = brighter)
                float intensity = 1 - (distance / maxDistance);  // 1 is full intensity, 0 is no intensity
                buttonImage.color = Color.Lerp(defaultColor, lightColor, intensity); // Blend between default and light color
            }
            else
            {
                // If the light is too far away, reset the button color to default
                buttonImage.color = defaultColor;
            }
        }
    }
}