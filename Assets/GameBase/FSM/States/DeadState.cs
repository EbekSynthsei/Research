using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeadState : State
{
    protected DeadStateData stateData;

    public DeadState(Entity _entity, FSM _stateMachine, string _animBoolName, DeadStateData _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        entity.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
