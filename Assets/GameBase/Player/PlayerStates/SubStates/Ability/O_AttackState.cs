using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_AttackState : AbilityState   
{
    //TODO: Controllare seconda arma, commentare tutto mannaggia a DIO
    private Weapon weapon;
    private float velocityToSet;
    private bool SetVelocity;
    private bool shouldFlip;
    public O_AttackState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetVelocity = false;
        weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (shouldFlip)
        {
            Core.movement.CheckIfShouldFlip(xInput);
        }
        if (SetVelocity)
        {
            Core.movement.SetVelocityFacingDirection(velocityToSet);
        }
    }

    public void SetWeapon(Weapon _weapon)
    {
        this.weapon = _weapon;
        weapon.InitializeWeapon(this);
    }
    public void SetPlayerVelocity(float velocity)
    {
        Core.movement.SetVelocityFacingDirection(velocity);
        velocityToSet = velocity;
        SetVelocity = true;
        
    }
    public void SetFlipCheck(bool value)
    {
        shouldFlip = value;
    }
    #region AnimationTriggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
    #endregion
}
