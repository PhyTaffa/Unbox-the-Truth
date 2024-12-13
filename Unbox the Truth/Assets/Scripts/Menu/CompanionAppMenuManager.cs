using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization; // For scene loading
using UnityEngine.UI;

public class CompanionAppMenuManager : MonoBehaviour
{
    // Add public buttons to connect them in the Inspector
    [Header("Companion App Button Manager")]
    [SerializeField] private Button skin1Button;
    [SerializeField] private Button skin2Button;
    [SerializeField] private Button skinDefaultButton;
    [SerializeField] private Button quitToMMButton;
    [SerializeField] private Button connectButton;
    [SerializeField] private Button disconnectButton;
    
    [Header("Sprites")]
    [SerializeField] private Sprite sprite1; // Set in Inspector for the play button sprite
    [SerializeField] private Sprite sprite2; // Set in Inspector for the options button sprite
    [SerializeField] private Sprite defaultSprite;

    
    private Dictionary<int, Sprite> playerSpriteDictionary;
    private bool a;
    // Start is called before the first frame update
    void Start()
    {
        
        skin1Button.interactable = false;
        skin2Button.interactable = false;
        
        
        // Add listeners to each button's onClick event
        skin1Button.onClick.AddListener(OnSkin1ButtonClicked);
        skin2Button.onClick.AddListener(OnSkin2ButtonClicked);
        skinDefaultButton.onClick.AddListener(OnSkinDefaultButtonClicked);
        quitToMMButton.onClick.AddListener(OnQuitToMMButtonClicked);
        
        //maneges conncetion
        connectButton.onClick.AddListener(OnConnectButtonClicked);
        disconnectButton.onClick.AddListener(OnDisconnectButtonClicked);

        
        
        playerSpriteDictionary = new Dictionary<int, Sprite>
        {
            { 0, defaultSprite },
            { 1, sprite1 },
            { 2, sprite2 }
        };
    }

    private void OnDisconnectButtonClicked()
    {
        skin1Button.interactable = false;
        skin2Button.interactable = false;
        
        //resets the skin to default
        SpriteManager.SetSprite(playerSpriteDictionary[0]);
        Debug.Log($"Stored sprite: {defaultSprite.name}");
    }

    private void OnConnectButtonClicked()
    {
        skin1Button.interactable = true;
        skin2Button.interactable = true;
    }

    private void OnSkin1ButtonClicked()
    {
        Debug.Log("SKin 1 Selected");
        SpriteManager.SetSprite(playerSpriteDictionary[1]);
        Debug.Log($"Stored sprite: {sprite1.name}");
        // Load the "Game" scene (replace with your actual scene name)
    }
    
    private void OnSkin2ButtonClicked()
    {
        Debug.Log("Skin 2 Selected");
        SpriteManager.SetSprite(playerSpriteDictionary[2]);
        Debug.Log($"Stored sprite: {sprite2.name}");
        // Load the "Game" scene (replace with your actual scene name)
    }
    
    
    private void OnSkinDefaultButtonClicked()
    {
        Debug.Log("Default skin selected");
        SpriteManager.SetSprite(defaultSprite);
        Debug.Log($"Stored sprite: {defaultSprite.name}");
        // Load the "Game" scene (replace with your actual scene name)
    }
    
    private void OnQuitToMMButtonClicked()
    {
        Debug.Log("Main Menu clicked");
        // Load the "Game" scene (replace with your actual scene name)
        SceneManager.LoadScene("MainMenu");
    }
    
}
