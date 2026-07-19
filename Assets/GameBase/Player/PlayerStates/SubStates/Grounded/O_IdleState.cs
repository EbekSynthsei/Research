using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_IdleState : GroundedState
{

    public O_IdleState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Core.movement.SetVelocityX(0.0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState) 
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if (yInput == -1)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            } 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
