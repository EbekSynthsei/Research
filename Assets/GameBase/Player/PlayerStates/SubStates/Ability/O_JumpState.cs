using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_JumpState : AbilityState
{
    private int amountOfJumpsLeft;
    public O_JumpState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
        amountOfJumpsLeft = Core.movement.jumpStateData.amountOfJumps;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        UseJumpInput();
        Core.movement.SetVelocityY(Core.movement.jumpStateData.jumpVelocity);
        DecreaseJumpsLeft();
        isAbilityDone = true;
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

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else return false;
    }

    public void ResetJumpsLeft()
    {
        amountOfJumpsLeft = Core.movement.jumpStateData.amountOfJumps;
    }
    public void DecreaseJumpsLeft()
    {
        amountOfJumpsLeft--;
    }
}
