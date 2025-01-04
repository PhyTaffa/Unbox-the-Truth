using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class TestCompanionApp : MonoBehaviour
{
    private int JsonLength = 3; // Number of buttons to create
    private Canvas canvas;           // Reference to the Canvas
    private GameObject[] buttonArray; // Array to store initial buttons
    private List<GameObject> buttonList; // List to store instantiated buttons
    private UnityEngine.UI.Button selectedButton; // Reference for button manipulations
    private Dictionary<int, string> buttonSpriteDictionary; // Dictionary for sprite data
    private Sprite selectedSprite; // Sprite used for disabled buttons
    private Dictionary<int, string> playerSpriteDictionary;
    private EndpointCalls ep;
    
    async void Start()
    {
        InitializeCanvas();
        InitializeButtonArray();
        InitializeDictionary();

        //InstantiateSpriteManagerSingleton();
        
        ep = new EndpointCalls();
        JsonLength = await ep.GetNumberChallenges();
        
        CreateButtons();
        TestButtonInteractions();
    }

    private void InstantiateSpriteManagerSingleton()
    {
        if (SpriteManagerSingleton.Instance == null)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/SpriteManager"));
            Debug.LogError("Sprite Manager Singleton Not Found");
        }
    }

    /// <summary>
    /// Finds and assigns the Canvas object.
    /// </summary>
    private void InitializeCanvas()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    /// <summary>
    /// Finds and assigns the initial set of buttons based on their tag.
    /// </summary>
    private void InitializeButtonArray()
    {
        buttonArray = GameObject.FindGameObjectsWithTag("Skin button");
        buttonList = new List<GameObject>();

        // Log the names of all found buttons
        foreach (var button in buttonArray)
        {
            //Debug.Log($"Found button: {button.name}");
        }
    }

    /// <summary>
    /// Populates the player sprite dictionary with test data.
    /// </summary>
    private void InitializeDictionary()
    {
        buttonSpriteDictionary = new Dictionary<int, string>
        {
            { 6, "Images/LegallyDistinctLogos/AKT-logo" },
            { 1, "Images/LegallyDistinctLogos/LocRx-logo" },
            { 2, "Images/LegallyDistinctLogos/pff-log" },
            { 3, "Images/LegallyDistinctLogos/tfr-logo" },
            { 4, "Images/LegallyDistinctLogos/Yangtze-logo" },
            { 5, "Images/amogus" },
            { 0, "Images/ritagliato 2" },
        };

        //missing actual sprites
        playerSpriteDictionary = new Dictionary<int, string>
        {
            { 6, "Images/LegallyDistinctLogos/AKT-logo" },
            { 1, "Images/LegallyDistinctLogos/LocRx-logo" },
            { 2, "Images/LegallyDistinctLogos/pff-log" },
            { 3, "Images/LegallyDistinctLogos/tfr-logo" },
            { 4, "Images/LegallyDistinctLogos/Yangtze-logo" },
            { 5, "Images/amogus" },
            { 0, "Images/ritagliato 2" },
        };

    }

    /// <summary>
/// Dynamically creates buttons in a grid layout and assigns functionality to them.
/// </summary>
private void CreateButtons()
{
    int columns = 2; // Number of columns in the grid
    float buttonWidth = 300f; // Width of each button
    float buttonHeight = 150f; // Height of each button
    float spacingX = 20f; // Horizontal spacing between buttons
    float spacingY = 20f; // Vertical spacing between buttons

    // Starting position for the grid (adjust as needed)
    Vector2 startPosition = new Vector2(440f, 100f);

    for (int i = 0; i < JsonLength; i++)
    {
        // Calculate row and column
        int row = i / columns;
        int column = i % columns;

        // Calculate button position
        Vector2 position = new Vector2(
            startPosition.x + (column * (buttonWidth + spacingX)),
            startPosition.y - (row * (buttonHeight + spacingY))
        );

        // Instantiate button
        GameObject newButton = Instantiate(buttonArray[0], canvas.transform);
        RectTransform newButtonRect = newButton.GetComponent<RectTransform>();
        newButtonRect.sizeDelta = new Vector2(buttonWidth, buttonHeight);
        newButtonRect.anchoredPosition = position;

        // Update button properties
        newButton.name = $"Skin button {i}";
        UnityEngine.UI.Button buttonComponent = newButton.GetComponent<UnityEngine.UI.Button>();
        TextMeshProUGUI buttonText = buttonComponent.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = $"Skin {i}";

        // Add click functionality
        int index = i;
        buttonComponent.onClick.AddListener(() =>
        {
            if (playerSpriteDictionary.TryGetValue(index, out string playerSpritePath))
            {
                Sprite loadedSprite = Resources.Load<Sprite>(playerSpritePath);
                SpriteManagerSingleton.Instance.SelectedSprite = loadedSprite;

                Debug.Log($"Skin {index} selected with sprite: {playerSpritePath}");
            }
        });

        // Add the button to the list
        buttonList.Add(newButton);
    }
}

    
    private async void TestButtonInteractions()
    {
        //List<bool> boolList = new List<bool>();
        //boolList = await ep.GetNumberChallengesWithUserUniqueId(44);
        
        for (int i = 0; i < buttonList.Count; i++)
        {
            var button = buttonList[i];
            selectedButton = button.GetComponent<UnityEngine.UI.Button>();

            selectedButton.interactable = false;

            if (buttonSpriteDictionary.TryGetValue(i, out string sprite))
            {
                selectedButton.image.sprite = Resources.Load<Sprite>(sprite);
            }
        }
    }

    internal void EnableButton(bool[] userChallengesCompleteStatus)
    {
        List<bool> challengeMet = new List<bool>();

        for (int i = 0; i < JsonLength; i++)
        {
            //Debug.Log(userChallengesCompleteStatus[i]);
            var button = buttonList[i];
            selectedButton = button.GetComponent<Button>();

            if (userChallengesCompleteStatus[i])
            {
                selectedButton.interactable = true;
            }
            else
            {
                selectedButton.interactable = false;
            }
        }
    }
}