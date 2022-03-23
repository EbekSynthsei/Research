using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected IdleStateData stateData;
    protected bool flipAfterIdle;
    protected float idleTime;
    protected bool isTimeIdleOver;
    protected bool isTargetInMinAggroRange;


    public IdleState(Entity _entity, FSM _stateMachine, string _animBoolName, IdleStateData _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTargetInMinAggroRange = Core.collisionSenses.TargetInMinAggroRange;
    }

    public override void Enter()
    {
        base.Enter();
        isTimeIdleOver = false;
        SetRandomIdleTime();
        Core.movement.SetVelocityFacingDirection(0.0f);
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle)
        {
            Core.movement.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= StartTime + idleTime)
        {
            isTimeIdleOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }
    public void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
