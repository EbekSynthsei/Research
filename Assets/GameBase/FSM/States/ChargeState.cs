using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected ChargeStateData stateData;

    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isTargetInMinAggroRange;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    public ChargeState(Entity _entity, FSM _stateMachine, string _animBoolName, ChargeStateData _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTargetInMinAggroRange = Core.collisionSenses.TargetInMinAggroRange;
        isDetectingLedge = Core.collisionSenses.Ledge;
        isDetectingWall = Core.collisionSenses.Wall;
        performCloseRangeAction = Core.collisionSenses.TargetInCloseRangeAction;
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;
        Core.movement.SetVelocityFacingDirection(stateData.chargeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= StartTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
