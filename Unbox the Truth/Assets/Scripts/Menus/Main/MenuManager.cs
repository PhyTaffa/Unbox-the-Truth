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

    private void PlaySound()
    {
        //m_MyAudioSource.Play();
    }
    
    // Load the scene corresponding to "Play"
    private void OnPlayButtonClicked()
    {
        PlaySound();

        SceneManager.LoadScene("HubLevel");
    }

    // Load the "Options" scene
    private void OnOptionsButtonClicked()
    {
        PlaySound();
        
        //SceneManager.LoadScene("Options");
    }

    // Open the Companion App (this could be a separate application or feature)
    private void OnCompanionAppButtonClicked()
    {
        PlaySound();
        SceneManager.LoadScene("CompanionAppMenu");
    }

    // Quit the game
    private void OnQuitButtonClicked()
    {
        PlaySound();
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

