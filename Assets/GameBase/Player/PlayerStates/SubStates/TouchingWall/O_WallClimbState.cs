using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_WallClimbState : TouchingWallState
{
    public O_WallClimbState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
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
        if (!isExitingState)
        {
            Core.movement.SetVelocityY(Core.movement.touchingWallData.wallClimbVelocity);
            if (yInput != 1)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
