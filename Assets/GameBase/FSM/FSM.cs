using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main class for the Finite State Machine pattern.
/// </summary>
public class FSM
{
    public State currentState { get; private set; }

    /// <summary>
    /// Initializes the state machine with the starting state.
    /// </summary>
    /// <param name="startingState">The initial state of the state machine.</param>
    public void Init(State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    /// <summary>
    /// Changes the current state to a new state.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
