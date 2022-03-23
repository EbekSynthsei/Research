using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected DodgeStateData stateData;

    protected bool performCloseRangeAction;
    protected bool isTargetInMaxAggroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;
    public DodgeState(Entity _entity, FSM _stateMachine, string _animBoolName, DodgeStateData _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTargetInMaxAggroRange = Core.collisionSenses.TargetInMaxAggroRange;
        performCloseRangeAction = Core.collisionSenses.TargetInCloseRangeAction;
        isGrounded = Core.collisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;

        Core.movement.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -Core.movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time>= StartTime +stateData.dodgeTime && isGrounded)
        {
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
