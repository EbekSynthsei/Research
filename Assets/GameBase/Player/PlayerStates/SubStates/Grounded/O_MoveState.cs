using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_MoveState : GroundedState
{
    public O_MoveState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
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
            
        Core.movement.CheckIfShouldFlip(xInput);
        Core.movement.SetVelocityX(Core.movement.moveStateData.moveSpeed * xInput);
        if (!isExitingState)
        {
            if (Core.movement.CurrentVelocity.x == 0.0f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if(yInput == -1)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
