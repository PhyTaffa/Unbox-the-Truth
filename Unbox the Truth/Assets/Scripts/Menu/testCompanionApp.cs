using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class TestCompanionApp : MonoBehaviour
{
    private int JsonLength = 3; // Number of buttons to create
    private Canvas canvas;           // Reference to the Canvas
    private GameObject[] buttonArray; // Array to store initial buttons
    private List<GameObject> buttonList; // List to store instantiated buttons
    private UnityEngine.UI.Button selectedButton; // Reference for button manipulations
    private Dictionary<int, string> playerSpriteDictionary; // Dictionary for sprite data
    private Sprite selectedSprite; // Sprite used for disabled buttons
    
    async void Start()
    {
        InitializeCanvas();
        InitializeButtonArray();
        InitializeDictionary();
        LoadResources();
        
        EndpointCalls ep = new EndpointCalls();
        JsonLength = await ep.GetNumberChallenges();
        
        CreateButtons();
        TestButtonInteractions(buttonList);
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
            Debug.Log($"Found button: {button.name}");
        }
    }

    /// <summary>
    /// Populates the player sprite dictionary with test data.
    /// </summary>
    private void InitializeDictionary()
    {
        playerSpriteDictionary = new Dictionary<int, string>
        {
            { 0, "sprite Default" },
            { 1, "sprite 1" },
            { 2, "sprite 2" },
            { 3, "sprite 3" },
            { 4, "sprite 4" },
            { 5, "sprite 5" },
            { 6, "sprite 6" },
            { 7, "sprite 7" },
        };
    }

    /// <summary>
    /// Loads required resources such as sprites.
    /// </summary>
    private void LoadResources()
    {
        //selectedSprite = Resources.Load<Sprite>("Images/amogus");
        selectedSprite = Resources.Load<Sprite>("Images/LegallyDistinctLogos/AKT-logo");
        //selectedSprite = Resources.Load<Sprite>("Images/brt-logo");
    }

    /// <summary>
    /// Dynamically creates buttons and assigns functionality to them.
    /// </summary>
    private void CreateButtons()
    {
        float buttonHeight = Screen.height / JsonLength;
        RectTransform buttonTemplateRect = buttonArray[0].GetComponent<RectTransform>();

        for (int i = 0; i < JsonLength; i++)
        {
            // Calculate position and instantiate button
            Vector2 position = new Vector2(canvas.transform.position.x, buttonHeight * i + buttonHeight / 2);
            GameObject newButton = Instantiate(buttonArray[0], position, buttonArray[0].transform.rotation, canvas.transform);

            // Adjust button size and name
            RectTransform newButtonRect = newButton.GetComponent<RectTransform>();
            newButtonRect.sizeDelta = new Vector2(buttonTemplateRect.sizeDelta.x, buttonHeight);
            newButton.name = $"Skin button {i}";

            // Assign text and click functionality
            UnityEngine.UI.Button buttonComponent = newButton.GetComponent<UnityEngine.UI.Button>();
            TextMeshProUGUI buttonText = buttonComponent.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = $"Skin {i}";

            // Capture the current index to avoid closure issues
            int index = i;
            buttonComponent.onClick.AddListener(() =>
            {
                if (playerSpriteDictionary.TryGetValue(index, out string sprite))
                {
                    Debug.Log($"Button {index} clicked. Sprite: {sprite}");
                }
                else
                {
                    Debug.Log($"Button {index} clicked. Sprite not found.");
                }
            });

            // Add the button to the list
            buttonList.Add(newButton);
            Debug.Log($"Created: {newButton.name}");
        }
    }

    /// <summary>
    /// Randomly disables or enables buttons and modifies their appearance.
    /// </summary>
    /// <param name="buttons">List of buttons to modify.</param>
    private void TestButtonInteractions(List<GameObject> buttons)
    {
        foreach (var button in buttons)
        {
            selectedButton = button.GetComponent<UnityEngine.UI.Button>();
            int randomValue = Random.Range(0, 10);

            if (randomValue < 5)
            {
                selectedButton.interactable = false;
                selectedButton.image.sprite = selectedSprite;
            }
            else
            {
                selectedButton.interactable = true;
            }
        }
    }
}