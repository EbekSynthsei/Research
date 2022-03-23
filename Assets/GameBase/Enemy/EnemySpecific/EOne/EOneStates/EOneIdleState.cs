using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOneIdleState : IdleState
{
    private EOne eOne;
    public EOneIdleState(Entity _entity, FSM _stateMachine, string _animBoolName, IdleStateData _stateData, EOne _eOne) : base(_entity, _stateMachine, _animBoolName, _stateData)
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
        else if (isTimeIdleOver)
        {
            stateMachine.ChangeState(eOne.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
