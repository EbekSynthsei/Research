using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    public InputManager inputManager;

    public bool EnableInputDebug;

    #region States

    //Grounded
    public O_IdleState IdleState { get; private set; }
    public O_MoveState MoveState { get; private set; }
    public O_LandState LandState { get; private set; }
    public O_CrouchIdleState CrouchIdleState { get; private set; }
    public O_CrouchMoveState CrouchMoveState { get; private set; }

    //In Air
    public InAirState InAirState { get; private set; }
    //LedgeClimb
    public LedgeClimbState LedgeClimbState { get; private set; }
    //Ability
    public O_JumpState JumpState { get; private set; }
    public O_WallJumpState WallJumpState { get; private set; }
    public O_DashState DashState { get; private set; }
    //Touching Wall
    public O_WallSlideState WallSlideState { get; private set; }
    public O_WallGrabState WallGrabState { get; private set; }
    public O_WallClimbState WallClimbState { get; private set; }

    //Attack
    public O_AttackState PrimaryAttackState { get; private set; }
    public O_AttackState SecondaryAttackState { get; private set; }
    #endregion

    [SerializeField]
    public PlayerInventory Inventory { get; private set; }

    public override void Start()
    {
        base.Start();

        inputManager = InputManager.Instance;
        Inventory = GetComponent<PlayerInventory>();
        //Grounded
        IdleState = new O_IdleState(this, stateMachine, "Idle", this);
        MoveState = new O_MoveState(this, stateMachine, "Move", this);
        LandState = new O_LandState(this, stateMachine, "Landing", this);
        CrouchIdleState = new O_CrouchIdleState(this, stateMachine, "CrouchIdle", this);
        CrouchMoveState = new O_CrouchMoveState(this, stateMachine, "CrouchMove", this);
        //In Air
        InAirState = new InAirState(this, stateMachine, "InAir", this);
        //LedgeClimb
        LedgeClimbState = new LedgeClimbState(this, stateMachine, "LedgeClimbState", this);
        //Ability
        JumpState = new O_JumpState(this, stateMachine, "InAir", this);
        WallJumpState = new O_WallJumpState(this, stateMachine, "InAir", this);
        DashState = new O_DashState(this, stateMachine, "Dash", this);
        //Touching Wall
        WallSlideState = new O_WallSlideState(this, stateMachine, "WallSlide", this);
        WallGrabState = new O_WallGrabState(this, stateMachine, "WallGrab", this);
        WallClimbState = new O_WallClimbState(this, stateMachine, "WallClimb", this);
        //Attack

        PrimaryAttackState = new O_AttackState(this, stateMachine, "Attack", this);
        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        SecondaryAttackState = new O_AttackState(this, stateMachine, "Attack", this);
        stateMachine.Init(IdleState);
    }

    public override void Update()
    {
        base.Update();
    }
}
