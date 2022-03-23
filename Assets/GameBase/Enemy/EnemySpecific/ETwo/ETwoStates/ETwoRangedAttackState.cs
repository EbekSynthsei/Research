using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETwoRangedAttackState : RangedAttackState
{
    private ETwo eTwo;
    public ETwoRangedAttackState(Entity _entity, FSM _stateMachine, string _animBoolName, Transform _attackPosition, RangedAttackData _stateData, ETwo _eTwo) : base(_entity, _stateMachine, _animBoolName, _attackPosition, _stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (isTargetInMinAggroRange)
            {
                stateMachine.ChangeState(eTwo.TargetDetectedState);
            }
            else
            {
                stateMachine.ChangeState(eTwo.SearchTargetState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
