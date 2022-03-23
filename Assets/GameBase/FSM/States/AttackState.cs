using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;

    protected bool isTargetInMinAggroRange;
    public AttackState(Entity _entity, FSM _stateMachine, string _animBoolName, Transform _attackPosition) : base(_entity, _stateMachine, _animBoolName)
    {
        attackPosition = _attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTargetInMinAggroRange = Core.collisionSenses.TargetInMinAggroRange;
    }

    public override void Enter()
    {
        base.Enter();
        entity.AttackFSM.attackState = this;
        Core.movement.SetVelocityFacingDirection(0.0f);
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
    public virtual void TriggerAttack()
    {

    }
    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
