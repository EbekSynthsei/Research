using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_WallSlideState : TouchingWallState
{
    public O_WallSlideState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            Core.movement.SetVelocityY(-Core.movement.touchingWallData.wallSlideVelocity);

            if (GrabInput && yInput!=-1)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
            else if (GrabInput && yInput == 1)
            {
                stateMachine.ChangeState(player.WallClimbState);
            }
        }
    }

}