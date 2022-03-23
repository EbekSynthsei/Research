using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOneMoveState : MoveState
{
    private EOne eOne;

    public EOneMoveState(Entity _entity, FSM _stateMachine, string _animBoolName, MoveStateData _stateData, EOne _eOne) : base(_entity, _stateMachine, _animBoolName, _stateData)
    {
        eOne = _eOne;
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
        else if(isDetectingWall || !isDetectingLedge)
        {
            eOne.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(eOne.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
