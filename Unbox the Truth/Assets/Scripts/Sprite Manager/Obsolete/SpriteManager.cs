using UnityEngine;

public static class SpriteManager
{
    // The currently selected sprite (can be set by the menu or game)
    public static Sprite selectedSprite;

    // Default sprite to be used if no sprite is selected
    public static Sprite defaultSprite;

    // Static constructor to ensure that default sprite is set at startup
    static SpriteManager()
    {
        // This is a fallback if you don't assign a sprite in the Inspector
        selectedSprite = Resources.Load<Sprite>("Images/player/Player_default_v2"); // This assumes you have a sprite named "ritagliato 2" in Resources folder
        if (selectedSprite == null)
        {
            Debug.LogError("Default sprite not found. Please assign a default sprite.");
        }
    }

    // Function to set the selected sprite (this could be based on button press)
    public static void SetSprite(Sprite sprite)
    {
        selectedSprite = sprite;
    }
}