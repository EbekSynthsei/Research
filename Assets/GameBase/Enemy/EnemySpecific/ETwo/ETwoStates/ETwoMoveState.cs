using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETwoMoveState : MoveState
{
    ETwo eTwo;
    public ETwoMoveState(Entity _entity, FSM _stateMachine, string _animBoolName, MoveStateData _stateData, ETwo _eTwo) : base(_entity, _stateMachine, _animBoolName, _stateData)
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
        else if (isDetectingWall || !isDetectingLedge || (isDetectingWall && isDetectingLedge))
        {
            eTwo.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(eTwo.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
