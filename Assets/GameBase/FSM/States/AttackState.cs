using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isTargetInMinAggroRange;

    public AttackState(Entity _entity, FSM _stateMachine, string _animBoolName, Transform _attackPosition)
        : base(_entity, _stateMachine, _animBoolName)
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
        // entity.AnimToFSM.thisState = this; è già fatto da base.Enter() in State.cs
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

    #region AnimationTriggers

    /// <summary>
    /// Chiamato dall'Animation Event "AnimationTrigger" sulla clip di attacco nemico.
    /// Sostituisce il vecchio TriggerAttack().
    /// </summary>
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    /// <summary>
    /// Chiamato dall'Animation Event "AnimationFinishTrigger" a fine clip.
    /// Sostituisce il vecchio FinishAttack(). isAnimationFinished viene già
    /// settato da base.AnimationFinishTrigger() in State.cs.
    /// </summary>
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    #endregion
}