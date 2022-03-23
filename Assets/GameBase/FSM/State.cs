using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// /MainClass for the Finite State Machine
/// </summary>
public class State 
{
    //Main Structure
    protected FSM stateMachine;
    protected Entity entity;
    protected CORE Core;
    //Example Process to State association
    protected string animBoolName;
    protected bool isAnimationFinished;
    public float StartTime { get; protected set; }

    public State(Entity _entity, FSM _stateMachine, string _animBoolName)
    {
        stateMachine = _stateMachine;
        entity = _entity;
        animBoolName = _animBoolName;
        Core = entity.Core;
    }
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
    public virtual void Exit()
    {
        entity.Anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
    public virtual void AnimationTrigger()
    {
    }
    public virtual void AnimationFinishTrigger() { isAnimationFinished = true; }
    public virtual void StateDebug()
    { 
            Debug.Log("<color=green>" + entity + "enters the : " + stateMachine.currentState + "</color>");
    }
}
