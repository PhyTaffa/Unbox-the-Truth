using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization; // For scene loading
using UnityEngine.UI;

public class CompanionAppMenuManager : MonoBehaviour
{
    // Add public buttons to connect them in the Inspector
    [SerializeField] private Button skin1Button;
    [SerializeField] private Button skin2Button;
    [SerializeField] private Button skinDefaultButton;
    [SerializeField] private Button quitToMMButton;
    
    [Header("Sprites")]
    [SerializeField] private Sprite sprite1; // Set in Inspector for the play button sprite
    [SerializeField] private Sprite sprite2; // Set in Inspector for the options button sprite
    [SerializeField] private Sprite defaultSprite;
    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to each button's onClick event
        skin1Button.onClick.AddListener(OnSkin1ButtonClicked);
        skin2Button.onClick.AddListener(OnSkin2ButtonClicked);
        skinDefaultButton.onClick.AddListener(OnSkinDefaultButtonClicked);
        quitToMMButton.onClick.AddListener(OnQuitToMMButtonClicked);
    }
    
    private void OnSkin1ButtonClicked()
    {
        Debug.Log("SKin 1 Selected");
        SpriteManager.SetSprite(sprite1);
        Debug.Log("Stored sprite: " + sprite1.name);
        // Load the "Game" scene (replace with your actual scene name)
    }
    
    private void OnSkin2ButtonClicked()
    {
        Debug.Log("Skin 2 Selected");
        SpriteManager.SetSprite(sprite2);
        Debug.Log("Stored sprite: " + sprite2.name);
        // Load the "Game" scene (replace with your actual scene name)
    }
    
    
    private void OnSkinDefaultButtonClicked()
    {
        Debug.Log("Default skin selected");
        SpriteManager.SetSprite(defaultSprite);
        Debug.Log("Stored sprite: " + defaultSprite.name);
        // Load the "Game" scene (replace with your actual scene name)
    }
    
    private void OnQuitToMMButtonClicked()
    {
        Debug.Log("Main Menu clicked");
        // Load the "Game" scene (replace with your actual scene name)
        SceneManager.LoadScene("MainMenu");
    }
    
}
