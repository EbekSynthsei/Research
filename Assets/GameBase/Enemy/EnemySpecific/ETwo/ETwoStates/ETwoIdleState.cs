using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETwoIdleState : IdleState
{
    private ETwo eTwo;

    public ETwoIdleState(Entity _entity, FSM _stateMachine, string _animBoolName, IdleStateData _stateData, ETwo _eTwo) : base(_entity, _stateMachine, _animBoolName, _stateData)
    {
        eTwo = _eTwo;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isTargetInMinAggroRange)
        {
            stateMachine.ChangeState(eTwo.TargetDetectedState);
        }
        else if (isTimeIdleOver)
        {
            stateMachine.ChangeState(eTwo.MoveState);
        }
    }
}
