using UnityEngine;
using UnityEngine.SceneManagement;  // For scene loading
using UnityEngine.UI;  // For UI button interaction
using TMPro;  // For TextMeshPro integration

public class MenuManager : MonoBehaviour
{
    // Add public buttons to connect them in the Inspector
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button companionAppButton;
    [SerializeField] private Button quitButton;
    //private AudioSource m_MyAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        //m_MyAudioSource = GetComponent<AudioSource>();
        
        // Add listeners to each button's onClick event
        playButton.onClick.AddListener(OnPlayButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);
        companionAppButton.onClick.AddListener(OnCompanionAppButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        
        
    }

    // Load the scene corresponding to "Play"
    private void OnPlayButtonClicked()
    {
        
        Debug.Log("Play button clicked");
        // Load the "Game" scene (replace with your actual scene name)
        SceneManager.LoadScene("HubLevel");
    }

    // Load the "Options" scene
    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options button clicked");
        //m_MyAudioSource.Play();

        //SceneManager.LoadScene("Options");
    }

    // Open the Companion App (this could be a separate application or feature)
    private void OnCompanionAppButtonClicked()
    {
        Debug.Log("Companion App button clicked");
        SceneManager.LoadScene("CompanionAppMenu");

        // Optionally, you can load the companion app scene, or open an external link
        // Example: SceneManager.LoadScene("CompanionApp");
    }

    // Quit the game
    private void OnQuitButtonClicked()
    {
        Debug.Log("Quit button clicked");
        // Quit the game (only works in a built application, not in the editor)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

