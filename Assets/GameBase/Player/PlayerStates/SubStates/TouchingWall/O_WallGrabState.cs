using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_WallGrabState : TouchingWallState
{
    public O_WallGrabState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Core.movement.holdPosition = entity.transform.position;
        Core.movement.HoldPosition();
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
            Core.movement.HoldPosition();
            if (yInput > 0 && GrabInput)
            {
                stateMachine.ChangeState(player.WallClimbState);
            }
            else if (yInput < 0 || !GrabInput)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
