using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a state specific to the player.
/// </summary>
public class O_State : State
{
    protected Player player;

    #region InputValues

    protected Vector2 inputVector;
    protected int xInput;
    protected int yInput;
    protected bool GrabInput;
    protected int NormInputX;
    protected int NormInputY;
    protected bool JumpInput;
    protected bool DashInput;
    protected bool[] AttackInputs;

    #endregion

    #region Generic Conditions

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingWallBack;
    protected bool isTouchingLedge;
    protected bool isTouchingBackLedge;
    protected bool isTouchingCeiling;
    protected bool isExitingState;

    #endregion

    /// <summary>
    /// Initializes a new instance of the O_State class.
    /// </summary>
    /// <param name="_entity">The entity associated with this state.</param>
    /// <param name="_stateMachine">The state machine managing this state.</param>
    /// <param name="_animBoolName">The animation boolean name associated with this state.</param>
    /// <param name="_player">The player associated with this state.</param>
    public O_State(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) 
        : base(_entity, _stateMachine, _animBoolName)
    {
        player = _player;
        AttackInputs = new bool[Enum.GetValues(typeof(CombatInputs)).Length];
    }

    /// <summary>
    /// Performs checks required by the state.
    /// </summary>
    public override void DoChecks()
    {
        base.DoChecks();
        
        isGrounded = Core.collisionSenses.Ground;
        isTouchingWall = Core.collisionSenses.Wall;
        isTouchingLedge = Core.collisionSenses.LedgeVault;
        isTouchingBackLedge = Core.collisionSenses.BackLedgeVault;
        isTouchingWallBack = Core.collisionSenses.WallBack;
        isTouchingCeiling = Core.collisionSenses.Ceiling;
    }

    /// <summary>
    /// Called when the state is entered.
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        player.AnimToFSM.thisState = this;
        isExitingState = false;
    }

    /// <summary>
    /// Called when the state is exited.
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        if (player.EnableInputDebug)
        {
            InputDebug();
        }
        isExitingState = true;
    }

    /// <summary>
    /// Called to update the logic of the state.
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        GetNormalizedInput();
        xInput = NormInputX;
        yInput = NormInputY;

        GetJumpInput();
        GetDashInput();
        GetGrabInput();
        GetAttackInput();
    }

    /// <summary>
    /// Called to update the physics of the state.
    /// </summary>
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    /// <summary>
    /// Retrieves normalized input values.
    /// </summary>
    public virtual void GetNormalizedInput()
    {
        NormInputX = InputManager.Instance.NormInputX();
        NormInputY = InputManager.Instance.NormInputY();
    }

    /// <summary>
    /// Retrieves attack input values.
    /// </summary>
    public virtual void GetAttackInput()
    {
        AttackInputs[(int)CombatInputs.primary] = InputManager.Instance.OnPrimaryAttackPressed();
        AttackInputs[(int)CombatInputs.secondary] = InputManager.Instance.OnSecondaryAttackPressed();
    }

    /// <summary>
    /// Retrieves jump input value.
    /// </summary>
    public virtual void GetJumpInput()
    {
        JumpInput = InputManager.Instance.OnJumpPressed();
    }

    /// <summary>
    /// Retrieves dash input value.
    /// </summary>
    public virtual void GetDashInput()
    {
        DashInput = InputManager.Instance.OnDashPressed();
    }

    /// <summary>
    /// Retrieves grab input value.
    /// </summary>
    public virtual void GetGrabInput()
    {
        GrabInput = InputManager.Instance.GrabInput();
    }

    /// <summary>
    /// Resets the jump input value.
    /// </summary>
    public virtual void UseJumpInput() => JumpInput = false;

    /// <summary>
    /// Resets the dash input value.
    /// </summary>
    public virtual void UseDashInput() => DashInput = false;

    /// <summary>
    /// Logs state debug information.
    /// </summary>
    public override void StateDebug()
    {
        base.StateDebug();
        Debug.Log($"<color=cyan>Entity is Facing: {Core.movement.FacingDirection}</color>\n" +
                  $"<color=cyan>Is Grounded: {isGrounded}</color>\n" +
                  $"<color=cyan>Is Touching Wall: {isTouchingWall}</color>\n" +
                  $"<color=cyan>Is Touching WallBack: {isTouchingWallBack}</color>\n" +
                  $"<color=cyan>Is Touching Ledge: {isTouchingLedge}</color>");
    }

    /// <summary>
    /// Logs input debug information.
    /// </summary>
    public virtual void InputDebug()
    {
        Debug.Log($"<color=cyan>INPUTS</color>\n" +
                  $"X Input: {xInput}\n" +
                  $"Y Input: {yInput}\n" +
                  $"Grab Input: {GrabInput}\n" +
                  $"Jump Input: {JumpInput}\n" +
                  $"Dash Input: {DashInput}\n" +
                  $"Primary Attack Input: {AttackInputs[0]}\n" +
                  $"Secondary Attack Input: {AttackInputs[1]}");
    }
}

/// <summary>
/// Enumeration for combat inputs.
/// </summary>
public enum CombatInputs
{
    primary,
    secondary
}
