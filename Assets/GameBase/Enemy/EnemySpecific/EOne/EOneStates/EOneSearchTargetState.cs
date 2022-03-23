using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOneSearchTargetState : SearchTargetState
{
    private EOne eOne;

    public EOneSearchTargetState(Entity _entity, FSM _stateMachine, string _animBoolName, SearchTargetData _stateData, EOne _eOne) : base(_entity, _stateMachine, _animBoolName, _stateData)
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
        if (isTargetInMinAggroRange)
        {
            stateMachine.ChangeState(eOne.TargetDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(eOne.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
