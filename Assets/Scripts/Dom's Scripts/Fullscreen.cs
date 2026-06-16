using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    public GameObject screenSettings;

    // Check which platform we're in and only show the settings if we're not on a mobile device
    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            screenSettings.SetActive(false);
        }

        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            screenSettings.SetActive(true);
        }
    }

    // Using a button event change the screen to full screen
    public void ChangeFullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    // Using a button event change the screen to a window
    public void ChangeWindow()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    // Using a button event change the screen to a full screened window
    public void ChangeFullWindow()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }
}
