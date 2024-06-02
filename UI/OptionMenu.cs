using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer; // Reference to the audio mixer for adjusting volume.

    [SerializeField]
    private TMP_Dropdown resolutionDropdown; // Dropdown UI element for selecting screen resolution.

    [SerializeField]
    private GameObject mainMenu; // Reference to the main menu UI.

    [SerializeField]
    private GameObject optionMenu; // Reference to the options menu UI.

    [SerializeField]
    private GameObject background; // Reference to the background object.

    private Resolution resolution; // Current screen resolution.
    private Resolution[] resolutions; // Array to store available screen resolutions.
    private string option; // String representing resolution option.
    private int currentResolutionIndex; // Index of the currently selected resolution.
    List<string> options = new List<string>(); // List to store resolution options.

    // Start is called before the first frame update.
    public void Start()
    {
        OptionsOfResolution(); // Initialize resolution options when the game starts.
    }

    // Populate dropdown with available screen resolutions.
    public void OptionsOfResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Set the main menu UI active or inactive based on button press.
    public void SetMainMenu(bool isPressed)
    {
        if (!isPressed)
        {
            optionMenu.SetActive(false);
            background.SetActive(true);
            mainMenu.SetActive(true);
        }
    }

    // Handle back button functionality.
    public void SetBack(bool isPressed)
    {
        if (!isPressed && background.activeInHierarchy == true)
        {
            optionMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            optionMenu.SetActive(false);
            Time.timeScale = 1F;
            PauseMenu.isPaused = false;
        }
    }

    // Set the audio volume using the audio mixer.
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    // Set whether the game runs in fullscreen mode.
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Set the screen resolution.
    public void SetResolution(int resolutionIndex)
    {
        resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
