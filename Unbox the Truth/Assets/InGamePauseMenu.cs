using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGamePauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;  // Reference to the pause menu panel (overlay)
    public Button resumeButton;   // Reference to the Resume button
    public Button optionsButton;  // Reference to the Options button
    public Button quitButton;     // Reference to the Quit button

    private bool isPaused = false;  // Track whether the game is paused

    void Start()
    {
        // Ensure the pause menu is hidden at the start
        pauseMenu.SetActive(false);

        // Add listeners for buttons (currently, we'll just print a message)
        resumeButton.onClick.AddListener(OnResume);
        optionsButton.onClick.AddListener(OnOptions);
        quitButton.onClick.AddListener(OnQuit);
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
        TogglePause();  // This will hide the menu and unpause the game
    }

    // Options button action (not implemented yet)
    private void OnOptions()
    {
        SceneManager.LoadScene("HubLevel");
        Debug.Log("Options button pressed.");
    }

    // Quit button action (not implemented yet)
    private void OnQuit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Quit button pressed.");
    }
}