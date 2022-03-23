using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeClimbState : O_State
{
    private Vector2 detectedPosition;
    private Vector2 cornerPosition;
    private Vector2 startPosition;
    private Vector2 stopPosition;
    private Vector2 workVector;
    private bool isHanging;
    private bool isClimbing;
    public LedgeClimbState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("LedgeClimb", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isHanging = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();


        Core.movement.SetVelocityZero();

        entity.transform.position = detectedPosition;

        cornerPosition = Core.collisionSenses.GetCornerPosition();

        startPosition.Set(cornerPosition.x - (Core.movement.FacingDirection * Core.movement.startOffset.x), cornerPosition.y - Core.movement.startOffset.y);
        stopPosition.Set(cornerPosition.x + (Core.movement.FacingDirection * Core.movement.stopOffset.x), cornerPosition.y + Core.movement.stopOffset.y);

        entity.transform.position = startPosition;
    }

    public override void Exit()
    {
        base.Exit();
        isHanging = false;
        if (isClimbing)
        {
            entity.transform.position = stopPosition;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isTouchingCeiling)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else 
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
        else {
            Core.movement.SetVelocityZero();

            entity.transform.position = startPosition;

            if (xInput == Core.movement.FacingDirection && isHanging && !isClimbing || xInput == Core.movement.FacingDirection && isHanging && !isClimbing && yInput == 1)
            {
                CheckForSpace();
                isClimbing = true;
                player.Anim.SetBool("LedgeClimb", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {

                stateMachine.ChangeState(player.InAirState);
            }
            else if (JumpInput && !isClimbing)
            {
                player.WallJumpState.SetWallJumpDirection();
                stateMachine.ChangeState(player.WallJumpState);
            }
        }
    }

    public void SetDetectedPosition(Vector2 pos)
    {
        detectedPosition = pos;
    }

    private void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(cornerPosition + (Vector2.up) + (Vector2.right * Core.movement.FacingDirection), Vector2.up, Core.movement.moveStateData.standColliderHeight, Core.collisionSenses.WhatIsGround);
    }
    
}
