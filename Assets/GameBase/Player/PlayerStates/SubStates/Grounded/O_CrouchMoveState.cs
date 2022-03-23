using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_CrouchMoveState : GroundedState
{
    public O_CrouchMoveState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Core.collisionSenses.SetColliderHeight(Core.movement.moveStateData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        Core.collisionSenses.SetColliderHeight(Core.movement.moveStateData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            Core.movement.SetVelocityFacingDirection(Core.movement.moveStateData.crouchSpeed);
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }
    }
}