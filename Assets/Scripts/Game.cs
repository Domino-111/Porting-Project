using System;
using System.Linq;
using UnityEngine;

public static class Game
{
    /// <summary>
    /// Describes the current status of gameplay
    /// </summary>
    public enum State
    {
        Menu,
        Play,
        Pause,
        Over
    }

    //this will hold our current state
    private static State stateCurrent = State.Menu;

    /// <summary>
    /// Get the current game state, or set a new state and trigger the onStateChange action.
    /// </summary>
    public static State CurrentState
    {
        get => stateCurrent;
        set
        {
            stateCurrent = value;
            //Changing CurrentState will automatically invoke the event with the new state
            onStateChange?.Invoke(stateCurrent);
        }
    }

    /// <summary>
    /// Returns true if gameplay is active (i.e. CurrentState == State.Play)
    /// </summary>
    public static bool IsPlaying => CurrentState == State.Play;

    /// <summary>
    /// This will trigger automatically when Game.CurrentState is updated. It will pass the current state.
    /// </summary>
    public static Action<State> onStateChange;

    /// <summary>
    /// This will trigger when Game.Over() is called.
    /// </summary>
    public static Action onGameOver;

    /// <summary>
    /// Call this method to end the current round.
    /// </summary>
    public static void Over()
    {
        //Invoke the event
        onGameOver?.Invoke();
        
        //Change the current state
        CurrentState = State.Over;
        
        //Stop everything
        StopAll();
    }

    public static void NewRound()
    {
        //Reset everything
        ResetAll();

        //Clear our current scrap
        Scrap.TryToSpend(Scrap.Amount);

        //Start gameplay
        CurrentState = State.Play;
    }

    private static void ResetAll()
    {
        foreach (var item in MonoBehaviour.FindObjectsOfType<MonoBehaviour>(true).OfType<IReset>())
        {
            item.Reset();
        }
    }

    private static void StopAll()
    {
        foreach (var item in MonoBehaviour.FindObjectsOfType<MonoBehaviour>(true).OfType<IStop>())
        {
            item.Stop();
        }
    }
}

public interface IReset
{
    public void Reset();
}

public interface IStop
{
    public void Stop();
}