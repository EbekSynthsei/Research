using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOneTargetDetectedState : TargetDetectedState
{
    private EOne eOne;
    public EOneTargetDetectedState(Entity _entity, FSM _stateMachine, string _animBoolName, TargetDetectedStateData _stateData, EOne _eOne) : base(_entity, _stateMachine, _animBoolName, _stateData)
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
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(eOne.MeleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(eOne.ChargeState);
        }
        else if (!isTargetInMaxAggroRange)
        {
            stateMachine.ChangeState(eOne.SearchTargetState);
        }
        else if (!isDetectingLedge)
        {
            Core.movement.Flip();
            stateMachine.ChangeState(eOne.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
