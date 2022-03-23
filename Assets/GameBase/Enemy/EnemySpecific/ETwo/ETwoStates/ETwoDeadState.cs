using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETwoDeadState : DeadState
{
    private ETwo eTwo;

    public ETwoDeadState(Entity _entity, FSM _stateMachine, string _animBoolName, DeadStateData _stateData, ETwo _eTwo) : base(_entity, _stateMachine, _animBoolName, _stateData)
    {
        eTwo = _eTwo;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
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
