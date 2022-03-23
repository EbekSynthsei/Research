using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_CrouchIdleState : GroundedState
{
    public O_CrouchIdleState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Core.movement.SetVelocityZero();
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
            if(xInput != 0)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

}