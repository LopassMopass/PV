using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Static variable to track whether the game is paused or not
    public static bool isPaused = false;

    // Reference to the option menu GameObject in the Unity Editor
    [SerializeField]
    private GameObject optionMenu;

    // Update is called once per frame
    public void Update()
    {
        // Check if the Escape key is pressed and the game is not already paused
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            // Activate the option menu GameObject
            optionMenu.SetActive(true);

            // Stop the time in the game, effectively pausing it
            Time.timeScale = 0F;

            // Set the isPaused flag to true
            isPaused = true;
        }
    }
}
