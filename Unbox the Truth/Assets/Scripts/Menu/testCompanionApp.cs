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
            Debug.Log($"Found button: {button.name}");
        }
    }

    /// <summary>
    /// Populates the player sprite dictionary with test data.
    /// </summary>
    private void InitializeDictionary()
    {
        buttonSpriteDictionary = new Dictionary<int, string>
        {
            { 0, "Images/LegallyDistinctLogos/AKT-logo" },
            { 1, "Images/LegallyDistinctLogos/LocRx-logo" },
            { 2, "Images/LegallyDistinctLogos/pff-log" },
            { 3, "Images/LegallyDistinctLogos/tfr-logo" },
            { 4, "Images/LegallyDistinctLogos/Yangtze-logo" },
            { 5, "Images/amogus" },
            { 6, "Images/ritagliato 2" },
        };

        //missing actual sprites
        playerSpriteDictionary = new Dictionary<int, string>
        {
            { 0, "Images/LegallyDistinctLogos/AKT-logo" },
            { 1, "Images/LegallyDistinctLogos/LocRx-logo" },
            { 2, "Images/LegallyDistinctLogos/pff-log" },
            { 3, "Images/LegallyDistinctLogos/tfr-logo" },
            { 4, "Images/LegallyDistinctLogos/Yangtze-logo" },
            { 5, "Images/amogus" },
            { 6, "Images/ritagliato 2" },
        };

    }

    /// <summary>
    /// Dynamically creates buttons and assigns functionality to them.
    /// </summary>
    private void CreateButtons()
    {
        float buttonHeight = 100f;
        RectTransform buttonTemplateRect = buttonArray[0].GetComponent<RectTransform>();

        for (int i = 0; i < JsonLength; i++)
        {
            // Calculate position and instantiate button
            // To do better
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
                if (buttonSpriteDictionary.TryGetValue(index, out string buttonSpritePath))
                {
                    Debug.Log($"Button {index} clicked. Sprite: {buttonSpritePath}");
                }
                else
                {
                    Debug.Log($"Button {index} clicked. Sprite not found.");
                }
                
                if(playerSpriteDictionary.TryGetValue(index, out string playerSpritePath))
                {
                    Sprite loadedSprite = Resources.Load<Sprite>(playerSpritePath);
                    SpriteManagerSingleton.Instance.SelectedSprite = loadedSprite;
                    
                    Debug.Log($"Loaded sprite in the singleton: {loadedSprite.name}");
                }
                
            });

            // Add the button to the list
            buttonList.Add(newButton);
            Debug.Log($"Created: {newButton.name}");
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

    internal void EnableButton(List<bool> userChallengesCompleteStatus)
    {
        List<bool> challengeMet = new List<bool>();

        for (int i = 0; i < JsonLength; i++)
        {
            Debug.Log(userChallengesCompleteStatus[i]);
            var button = buttonList[i];
            selectedButton = button.GetComponent<Button>();

            if (userChallengesCompleteStatus[i])
            {
                selectedButton.interactable = true;
            }
        }
    }
}