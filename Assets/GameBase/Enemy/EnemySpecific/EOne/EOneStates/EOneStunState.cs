using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOneStunState : StunState
{
    private EOne eOne;
    public EOneStunState(Entity _entity, FSM _stateMachine, string _animBoolName, StunStateData _stateData, EOne _eOne) : base(_entity, _stateMachine, _animBoolName, _stateData)
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
        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(eOne.MeleeAttackState);
            }
            else if (isTargetInMinAggroRange)
            {
                stateMachine.ChangeState(eOne.ChargeState);
            }
            else
            {
                eOne.SearchTargetState.SetTurnImmediately(true);
                stateMachine.ChangeState(eOne.SearchTargetState);
            }        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
