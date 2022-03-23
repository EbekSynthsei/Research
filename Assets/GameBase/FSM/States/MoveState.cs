using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected MoveStateData stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isTargetInMinAggroRange;
    public MoveState(Entity _entity, FSM _stateMachine, string _animBoolName, MoveStateData _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingWall = Core.collisionSenses.Wall;
        isDetectingLedge = Core.collisionSenses.Ledge;
        isTargetInMinAggroRange = Core.collisionSenses.TargetInMinAggroRange;
    }

    public override void Enter()
    {
        base.Enter();
        Core.movement.SetVelocityFacingDirection(stateData.moveSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
