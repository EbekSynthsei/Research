using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_DashState : AbilityState
{
    public bool CanDash { get; private set; }
    private bool isHolding;
    private float lastDashTime;

    private Vector2 dashDirection;
    public O_DashState(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isHolding = true;
        CanDash = false;
        UseDashInput();
        dashDirection = Vector2.right * Core.movement.FacingDirection;
        Time.timeScale = Core.movement.dashStateData.holdTimeScale;
        StartTime = Time.unscaledTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            if (isHolding)
            {
                if (Time.unscaledTime >= StartTime + Core.movement.dashStateData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    StartTime = Time.time;
                    Core.movement.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    Core.movement.Rb.drag = Core.movement.dashStateData.drag;
                    Core.movement.SetVelocity(Core.movement.dashStateData.dashVelocity, dashDirection);
                }
            }
            else
            {
                Core.movement.SetVelocity(Core.movement.dashStateData.dashVelocity, dashDirection);
                if(Time.time >= StartTime + Core.movement.dashStateData.dashTime || isTouchingWall)
                {
                    Core.movement.Rb.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + Core.movement.dashStateData.dashCooldown;
    }
    public void ResetCanDash() { CanDash = true; }
}
