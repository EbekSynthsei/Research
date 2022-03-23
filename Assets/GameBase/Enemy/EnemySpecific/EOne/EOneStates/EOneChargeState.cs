using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOneChargeState : ChargeState
{
    private EOne eOne;

    public EOneChargeState(Entity _entity, FSM _stateMachine, string _animBoolName, ChargeStateData _stateData, EOne _eOne) : base(_entity, _stateMachine, _animBoolName, _stateData)
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
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(eOne.MeleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(eOne.SearchTargetState);
        }
        else if (isChargeTimeOver)
        {
            if (isTargetInMinAggroRange)
            {
                stateMachine.ChangeState(eOne.TargetDetectedState);
            }
            else
            {
                stateMachine.ChangeState(eOne.SearchTargetState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
