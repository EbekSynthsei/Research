using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public O_State(Entity _entity, FSM _stateMachine, string _animBoolName, Player _player) : base(_entity, _stateMachine, _animBoolName)
    {
        player = _player;
        AttackInputs = new bool[Enum.GetValues(typeof(CombatInputs)).Length];
    }

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

    public override void Enter()
    {
        base.Enter();
        player.AnimToFSM.thisState = this;
        isExitingState = false;
    }

    public override void Exit()
    {
        base.Exit();
        if (player.EnableInputDebug)
        {
            InputDebug();
        }
        isExitingState = true;
    }

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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    /// <summary>
    /// INPUT MANAGEMENT FROM THE STATE: This allow us to Check Constantly On The Input.
    /// The Input Handler Exists apart from the Player, so we need to get his data.
    /// We can get the infos for all the States From Here.
    /// A similar Pattern can be used for the Enemy AI. A NAV MESH AGENT IS RECOMMENDED
    /// </summary>
    /// 

    public virtual void GetNormalizedInput()
    {
        NormInputX = InputManager.Instance.NormInputX();
        NormInputY = InputManager.Instance.NormInputY();
    }
    public virtual void GetAttackInput()
    {
        AttackInputs[(int)CombatInputs.primary] = InputManager.Instance.OnPrimaryAttackPressed();
        AttackInputs[(int)CombatInputs.secondary] = InputManager.Instance.OnSecondaryAttackPressed();
    }
    public virtual void GetJumpInput()
    {
        JumpInput = InputManager.Instance.OnJumpPressed();
    }
    public virtual void GetDashInput()
    {
        DashInput = InputManager.Instance.OnDashPressed();
    }
    public virtual void GetGrabInput()
    {
        GrabInput = InputManager.Instance.GrabInput();
    }
    public virtual void UseJumpInput() => JumpInput = false;

    public virtual void UseDashInput() => DashInput = false;


    public override void StateDebug()
    {
        base.StateDebug();

        Debug.Log("Entity is Facing : " + Core.movement.FacingDirection +
                "<b> Is Grounded </b>" + isGrounded +
                "<b> Is Touching Wall : </b>" + isTouchingWall +
                "<b> Is Touching WallBack </b>" + isTouchingWallBack +
                "<b> Is Touching Ledge </b>" + isTouchingLedge);
    }
    public virtual void InputDebug()
    {
        Debug.Log( "<color=cyan>INPUTS</color>"
            + "X Input : " + xInput + " "
            + "Y Input : " + yInput + " "
            + "Grab Input : " + GrabInput + " "
            + "JumpInput : " + JumpInput + " "
            + "DashInput : " + DashInput + " "
            + "PrimaryAttackInput : " + AttackInputs[0] + " "
            + "Secondary Attack Input : " + AttackInputs[1]); 
    }
}
public enum CombatInputs
{
    primary,
    secondary
}
