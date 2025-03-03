using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main class for the Finite State Machine.
/// </summary>
public class State 
{
    // Main Structure
    protected FSM stateMachine;
    protected Entity entity;
    protected CORE Core;
    // Example Process to State association
    protected string animBoolName;
    protected bool isAnimationFinished;
    public float StartTime { get; protected set; }

    /// <summary>
    /// Initializes a new instance of the State class.
    /// </summary>
    /// <param name="_entity">The entity associated with this state.</param>
    /// <param name="_stateMachine">The state machine managing this state.</param>
    /// <param name="_animBoolName">The animation boolean name associated with this state.</param>
    public State(Entity _entity, FSM _stateMachine, string _animBoolName)
    {
        stateMachine = _stateMachine;
        entity = _entity;
        animBoolName = _animBoolName;
        Core = entity.Core;
    }

    /// <summary>
    /// Called when the state is entered.
    /// </summary>
    public virtual void Enter()
    {
        DoChecks();
        if (entity.EnableStateDebug)
        {
            StateDebug();
        }

        StartTime = Time.time;
        entity.Anim.SetBool(animBoolName, true);
        entity.AnimToFSM.thisState = this;
        isAnimationFinished = false;
    }

    /// <summary>
    /// Called when the state is exited.
    /// </summary>
    public virtual void Exit()
    {
        entity.Anim.SetBool(animBoolName, false);
    }

    /// <summary>
    /// Called to update the logic of the state.
    /// </summary>
    public virtual void LogicUpdate()
    {
    }

    /// <summary>
    /// Called to update the physics of the state.
    /// </summary>
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    /// <summary>
    /// Performs checks required by the state.
    /// </summary>
    public virtual void DoChecks()
    {
    }

    /// <summary>
    /// Triggered by an animation event.
    /// </summary>
    public virtual void AnimationTrigger()
    {
    }

    /// <summary>
    /// Triggered when an animation finishes.
    /// </summary>
    public virtual void AnimationFinishTrigger() 
    { 
        isAnimationFinished = true; 
    }

    /// <summary>
    /// Logs state debug information.
    /// </summary>
    public virtual void StateDebug()
    { 
        Debug.Log($"<color=green>{entity} enters the state: <color=yellow>{stateMachine.currentState}</color></color>");
    }
}
