using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected StunStateData stateData;
    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isTargetInMinAggroRange;
    public StunState(Entity _entity, FSM _stateMachine, string _animBoolName, StunStateData _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = Core.collisionSenses.Ground;
        performCloseRangeAction = Core.collisionSenses.TargetInCloseRangeAction;
        isTargetInMinAggroRange = Core.collisionSenses.TargetInMinAggroRange;
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
        isMovementStopped = false;
        Core.movement.SetVelocity(stateData.stunKnockBackSpeed, stateData.stunAngleKnockBack, entity.LastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        //entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isGrounded && Time.time >= StartTime + stateData.stunKnockBackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            Core.movement.SetVelocityFacingDirection(0.0f);
        }
        if(Time.time >= StartTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
