using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETwoStunState : StunState
{
    private ETwo eTwo;

    public ETwoStunState(Entity _entity, FSM _stateMachine, string _animBoolName, StunStateData _stateData, ETwo _eTwo) : base(_entity, _stateMachine, _animBoolName, _stateData)
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
        if (isStunTimeOver)
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
}
