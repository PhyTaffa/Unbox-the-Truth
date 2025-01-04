using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGamePauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;  // Reference to the pause menu panel (overlay)
    [SerializeField] private Button resumeButton;   // Reference to the Resume button
    [SerializeField] private Button restartButton;
    [SerializeField] private Button hubLevelButton;  // Reference to the Options button
    [SerializeField] private Button quitButton;     // Reference to the Quit button

    private bool isPaused = false;  // Track whether the game is paused

    private genericAudioPlayer gap;
    void Start()
    {
        // Ensure the pause menu is hidden at the start
        pauseMenu.SetActive(false);

        // Add listeners for buttons (currently, we'll just print a message)
        resumeButton.onClick.AddListener(OnResume);
        hubLevelButton.onClick.AddListener(OnHubLevel);
        restartButton.onClick.AddListener(OnRestart);
        quitButton.onClick.AddListener(OnQuit);
        
        gap = GetComponent<genericAudioPlayer>();
    }

    void Update()
    {
        // Check for the "Esc" key press
        if (pauseMenu != null && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Toggle the pause state
    private void TogglePause()
    {
        gap.m_MyAudioSource.Play();
        isPaused = !isPaused;

        if (isPaused)
        {
            // Show the pause menu
            pauseMenu.SetActive(true);

            // Pause the game time
            Time.timeScale = 0f;
        }
        else
        {
            // Hide the pause menu
            pauseMenu.SetActive(false);

            // Resume the game time
            Time.timeScale = 1f;
        }
    }

    // Resume button action (for now, just hide the menu and unpause)
    private void OnResume()
    {
        gap.m_MyAudioSource.Play();
        TogglePause();  // This will hide the menu and unpause the game
    }

    async void OnRestart()
    {
        gap.m_MyAudioSource.Play();
        //Lazy aaaaaaaah way to assure sound is played
        await Task.Delay(90);
        
        TogglePause();
        Scene curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.name);
    }
    
    // Options button action (not implemented yet)
    private void OnHubLevel()
    {
        gap.m_MyAudioSource.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene("HubLevel");
        Debug.Log("Options button pressed.");
    }

    // Quit button action (not implemented yet)
    private void OnQuit()
    {
        gap.m_MyAudioSource.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Quit button pressed.");
    }
}