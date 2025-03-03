using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the interact state for the player.
/// </summary>
public class O_InteractState : AbilityState
{
    public O_InteractState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) 
        : base(_entity, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Perform interaction logic here
        Interact();
        isAbilityDone = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)
        {
            if (isGrounded && Core.movement.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    private void Interact()
    {
        // Implement interaction logic here
        Debug.Log("Interacting with object");
    }
}
