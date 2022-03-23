using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_WallJumpState : AbilityState
{
    private int wallJumpDirection;
    public O_WallJumpState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        UseJumpInput();
        player.JumpState.ResetJumpsLeft();

        Core.movement.SetVelocity(Core.movement.touchingWallData.wallJumpVelocity, Core.movement.touchingWallData.wallJumpAngle, wallJumpDirection);
        Core.movement.CheckIfShouldFlip(wallJumpDirection);

        player.JumpState.DecreaseJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= StartTime + Core.movement.touchingWallData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetWallJumpDirection()
    {
        wallJumpDirection = -Core.movement.FacingDirection;
    }
}
