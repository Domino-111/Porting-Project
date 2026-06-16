using UnityEngine;

public class DesktopQuit : MonoBehaviour
{
    public GameObject quitButton;

    // Check if the platform used for the game is windows or not
    void Awake()
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer)
        {
            quitButton.SetActive(false);
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer) //|| Application.platform == RuntimePlatform.WindowsEditor)
        {
            quitButton.SetActive(true);
        }
    }

    // Quit the application via a button event
    public void QuitDesktop()
    {
        Application.Quit();
        print("Quit the game");
    }
}
