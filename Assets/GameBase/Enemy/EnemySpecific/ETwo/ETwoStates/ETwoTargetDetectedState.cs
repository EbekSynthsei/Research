using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETwoTargetDetectedState : TargetDetectedState
{
    private ETwo eTwo;

    public ETwoTargetDetectedState(Entity _entity, FSM _stateMachine, string _animBoolName, TargetDetectedStateData _stateData, ETwo _eTwo) : base(_entity, _stateMachine, _animBoolName, _stateData)
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
        if (performCloseRangeAction)
        {
            if (Time.time >= eTwo.DodgeState.StartTime + eTwo.dodgeStateData.dodgeCooldown && performCloseRangeAction)
            {
                stateMachine.ChangeState(eTwo.DodgeState);
            }
            else
            {
                stateMachine.ChangeState(eTwo.MeleeAttackState);
            }
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(eTwo.RangedAttackState);
        }
        else if(!isTargetInMaxAggroRange)
        {
            stateMachine.ChangeState(eTwo.SearchTargetState) ;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
