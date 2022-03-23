using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main class Finite State Machine Pattern
/// </summary>
public class FSM 
{
    public State currentState { get; private set; }

    public void Init(State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }
    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
