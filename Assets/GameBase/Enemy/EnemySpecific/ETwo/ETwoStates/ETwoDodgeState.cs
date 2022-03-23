using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETwoDodgeState : DodgeState
{
    private ETwo eTwo;
    public ETwoDodgeState(Entity _entity, FSM _stateMachine, string _animBoolName, DodgeStateData _stateData, ETwo _eTwo) : base(_entity, _stateMachine, _animBoolName, _stateData)
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
        if (isDodgeOver)
        {
            if (isTargetInMaxAggroRange && performCloseRangeAction)
            {
                stateMachine.ChangeState(eTwo.MeleeAttackState);
            }
            else if (!isTargetInMaxAggroRange)
            {
                stateMachine.ChangeState(eTwo.SearchTargetState);
            }
            else if (isTargetInMaxAggroRange && !performCloseRangeAction)
            {
                stateMachine.ChangeState(eTwo.RangedAttackState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
