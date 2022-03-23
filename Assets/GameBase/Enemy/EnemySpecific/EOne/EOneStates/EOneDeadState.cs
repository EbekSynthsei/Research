using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOneDeadState : DeadState
{
    private EOne eOne;

    public EOneDeadState(Entity _entity, FSM _stateMachine, string _animBoolName, DeadStateData _stateData, EOne _eOne) : base(_entity, _stateMachine, _animBoolName, _stateData)
    {
        eOne = _eOne;
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
