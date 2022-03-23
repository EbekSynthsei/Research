using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingWallState : O_State
{
    public TouchingWallState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
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
        if (JumpInput)
        {
            player.WallJumpState.SetWallJumpDirection();
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (isGrounded && !GrabInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || (xInput != Core.movement.FacingDirection && !GrabInput))
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
