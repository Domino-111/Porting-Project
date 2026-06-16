using Unity.VectorGraphics;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Tooltip("The game object holding the gameplay HUD elements")]
    [SerializeField] private GameObject gameScreen;
    [Tooltip("The game object holding the game over UI elements")]
    [SerializeField] private GameObject endScreen;

    void Start()
    {
        //Bind to the game over event
        Game.onGameOver += DisplayEndScreen;
    }

    private void OnDestroy()
    {
        //Unbind from the event if we get destroyed
        Game.onGameOver -= DisplayEndScreen;
    }

    private void DisplayEndScreen()
    {
        //Disable the game screen and enable the end screen.
        gameScreen.SetActive(false);
        endScreen.SetActive(true);
    }

    public void NewGame()
    {
        //Trigger a new round
        Game.NewRound();
    }
}
