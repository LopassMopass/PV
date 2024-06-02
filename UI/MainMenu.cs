using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Reference to the main menu panel
    [SerializeField] private GameObject mainMenu;
    // Reference to the options menu panel
    [SerializeField] private GameObject optionMenu;
    // Reference to the background object

    [SerializeField] private GameObject background;

    // Method to set up the play state
    public void SetPlay(bool isPressed)
    {
        // If play button is not pressed
        if (!isPressed)
        {
            // Disable the background
            background.SetActive(false);
            // Disable the main menu panel
            mainMenu.SetActive(false);
            // Set the time scale to normal (unpause)
            Time.timeScale = 1F;
            // Set the game as not paused
            PauseMenu.isPaused = false;
        }
    }

    // Method to switch to the options menu
    public void SetOptionMenu(bool isPressed)
    {
        // If the option menu button is not pressed
        if (!isPressed)
        {
            // Disable the main menu panel
            mainMenu.SetActive(false);
            // Enable the options menu panel
            optionMenu.SetActive(true);
        }
    }

    // Method to exit the game
    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
