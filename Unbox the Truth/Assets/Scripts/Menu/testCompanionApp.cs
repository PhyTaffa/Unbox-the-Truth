using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class testCompanionApp : MonoBehaviour
{
    private readonly int jsonLength = 7;
    private Canvas canvas;
    // Start is called before the first frame update
    private GameObject[] buttonArray;
    private List<GameObject> buttonList;
    
    private UnityEngine.UI.Button  selectedButton;
    private Dictionary<int, String> playerSpriteDictionary = new Dictionary<int, String>();

    private Sprite selectedSprite;
    
    void Start()
    {
        //canvas = gameObject.GetComponent<Canvas>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        
        buttonArray = GameObject.FindGameObjectsWithTag("Skin button");
        //buttonArray = currButton;
        buttonList = new List<GameObject>();
        
        foreach (var currButton in buttonArray)
        {
            Debug.Log(currButton.name);
        }

        int canvasHeght = Screen.height;
        RectTransform buttonSelectedRectTransf = buttonArray[0].GetComponent<RectTransform>();
        float currButtonHeight = buttonSelectedRectTransf.sizeDelta.y;
        float currButtonY = Screen.height/ jsonLength;

        
                
        playerSpriteDictionary = new Dictionary<int, String>
        {
            { 0, "defaultSprite" },
            { 1, "sprite1" },
            { 2, "sprite2" }
        };

        selectedSprite = Resources.Load<Sprite>("Images/amogus");
         
        // for (int i = 0; i < jsonLength; i++)
        // {
        //     
        //     Vector2 currPosition = new Vector2(canvas.transform.position.x, currButtonY * i + currButtonY/2);
        //     
        //     GameObject currButton = Instantiate(buttonArray[0], currPosition, buttonArray[0].transform.rotation, canvas.transform);
        //     
        //     currButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSelectedRectTransf.sizeDelta.x, currButtonY);
        //     //currButton.transform.GetChild(0).GetComponent<Image>().sprite = currButton.GetComponent<Image>().sprite;
        //     
        //     currButton.name = "Skin button " + i;
        //     //currButton.transform.SetParent(canvas.transform, false);
        //
        //     //currButton.transform.position.Set(buttonArray[0].transform.position.x, i* currButtonHeight, 0);
        //     
        //     //Button aButton = new Button();
        //     //aButton.name = "Button" + i;
        //     
        //     
        //     //canvas.transform.IsChildOf(aButton.Children());
        //     buttonList.Add(currButton);
        //     Debug.Log(currButton.name);    
        //     
        // }
        
        for (int i = 0; i < jsonLength; i++)
        {
            // Calculate the position for the new button
            Vector2 currPosition = new Vector2(canvas.transform.position.x, currButtonY * i + currButtonY / 2);

            // Instantiate a new button from the array
            GameObject currButton = Instantiate(buttonArray[0], currPosition, buttonArray[0].transform.rotation, canvas.transform);

            // Adjust button size
            currButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSelectedRectTransf.sizeDelta.x, currButtonY);

            // Set button name
            currButton.name = $"Skin button {i}";

            // Get the Button component
            UnityEngine.UI.Button buttonComponent = currButton.GetComponent<UnityEngine.UI.Button>();
            TextMeshProUGUI text = buttonComponent.GetComponentInChildren<TextMeshProUGUI>();
            
            text.text = $"Skin {i}";
            // Capture the current index to bind it to the event
            int index = i; // Important to avoid closure issues

            // Add the event to the button's OnClick
            buttonComponent.onClick.AddListener(() =>
            {
                if (playerSpriteDictionary.TryGetValue(index, out String sprite))
                {
                    Debug.Log($"Button {index} clicked. Sprite: {sprite}");
                    // Example action: change the player's sprite
                    // playerSpriteRenderer.sprite = sprite;
                }
                else
                {
                    Debug.Log($"Button {index} clicked. Sprite not found in dictionary.");
                }
            });

            // Add the button to the list
            buttonList.Add(currButton);

            Debug.Log(currButton.name);
        }

        
        Testicle(buttonList);
    }

    private void Testicle(List<GameObject> buttons)
    {
        foreach (var button in buttons)
        {
            selectedButton = button.GetComponent<UnityEngine.UI.Button >();
            
            int r = Random.Range(0, 10);
            if (r < 5)
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
