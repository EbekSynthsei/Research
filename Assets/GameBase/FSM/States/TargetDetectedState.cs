using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetectedState : State
{
    protected TargetDetectedStateData stateData;

    protected bool isTargetInMinAggroRange;
    protected bool isTargetInMaxAggroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;
    public TargetDetectedState(Entity _entity, FSM _stateMachine, string _animBoolName, TargetDetectedStateData _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTargetInMinAggroRange = Core.collisionSenses.TargetInMinAggroRange;
        isTargetInMaxAggroRange = Core.collisionSenses.TargetInMaxAggroRange;
        isDetectingLedge = Core.collisionSenses.Ledge;
        performCloseRangeAction = Core.collisionSenses.TargetInCloseRangeAction;

    }

    public override void Enter()
    {
        base.Enter();
        performLongRangeAction = false;
        Core.movement.SetVelocityFacingDirection(0.0f);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= StartTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
