using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETwoSearchTargetState : SearchTargetState
{
    private ETwo eTwo;
    public ETwoSearchTargetState(Entity _entity, FSM _stateMachine, string _animBoolName, SearchTargetData _stateData, ETwo _eTwo) : base(_entity, _stateMachine, _animBoolName, _stateData)
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
        if (isTargetInMinAggroRange)
        {
            stateMachine.ChangeState(eTwo.TargetDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(eTwo.MoveState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
