using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : O_State
{
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;


    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    public InAirState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
    }

    public override void DoChecks()
    {

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;
        base.DoChecks();
        isTouchingLedge = Core.collisionSenses.LedgeVault;
        isTouchingBackLedge = Core.collisionSenses.BackLedgeVault;

        if(isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(entity.transform.position);
        }
        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }

    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();
        if (AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        else if (isGrounded && Core.movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if(isTouchingWall && !isTouchingLedge && !isGrounded && yInput !=-1)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if(JumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();

            isTouchingWall = Core.collisionSenses.Wall;
            isTouchingWallBack = Core.collisionSenses.WallBack;

            player.WallJumpState.SetWallJumpDirection();

            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if(isTouchingWall && isTouchingLedge && GrabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && xInput == Core.movement.FacingDirection && Core.movement.CurrentVelocity.y <= 0f)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if(DashInput && player.DashState.CheckIfCanDash() && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.DashState);
        }
        else
        {
            Core.movement.CheckIfShouldFlip(xInput);
            Core.movement.SetVelocityX(Core.movement.moveStateData.moveSpeed * xInput);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime == true && Time.time >= StartTime + Core.movement.inAirStateData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseJumpsLeft();
        }
    }
    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime == true && Time.time >= StartTime + Core.movement.inAirStateData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime()
    {
        coyoteTime = true;
    }

    public void StartWallJumpCoyoteTime()
    { 
        wallJumpCoyoteTime = true;
    }
    public void StopWallJumpCoyoteTime() 
    { 
        wallJumpCoyoteTime = false;
    }
}
