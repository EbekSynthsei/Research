using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOne : Entity
{
    #region States
    public EOneIdleState IdleState { get; private set; }
    public EOneMoveState MoveState { get; private set; }
    public EOneTargetDetectedState TargetDetectedState { get; private set; }
    public EOneChargeState ChargeState { get; private set; }
    public EOneSearchTargetState SearchTargetState { get; private set; }
    public EOneMeleeAttackState MeleeAttackState { get; private set; }
    public EOneStunState StunState { get; private set; }
    public EOneDeadState DeadState { get; private set; }
    #endregion

    #region State Datas
    [Header("StateData")]
    [SerializeField]
    private IdleStateData idleData;
    [SerializeField]
    private MoveStateData moveData;

    [SerializeField]
    private TargetDetectedStateData targetDetectedData;
    [SerializeField]
    private ChargeStateData chargeStateData;
    [SerializeField]
    private SearchTargetData searchTargetData;

    [Header("Combat Data")]
    [SerializeField]
    private MeleeAttackData meleeAttackStateData;
    [SerializeField]
    private StunStateData stunStateData;
    [SerializeField]
    private Transform meleeAttackPosition;

    [Header("Damage Data")]
    [SerializeField]
    private DeadStateData deadStateData;
    #endregion


    public override void Start()
    {
        base.Start();

        //Move
        MoveState = new EOneMoveState(this, stateMachine, "Move", moveData, this);
        IdleState = new EOneIdleState(this, stateMachine, "Idle", idleData, this);
        //SearchAndAttack
        TargetDetectedState = new EOneTargetDetectedState(this, stateMachine, "Detecting", targetDetectedData, this);
        SearchTargetState = new EOneSearchTargetState(this, stateMachine, "Searching", searchTargetData, this);
        
        ChargeState = new EOneChargeState(this, stateMachine, "Charge", chargeStateData, this);
        MeleeAttackState = new EOneMeleeAttackState(this, stateMachine, "MeleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        //Damage
        StunState = new EOneStunState(this, stateMachine, "Stunned", stunStateData, this);
        DeadState = new EOneDeadState(this, stateMachine, "Dead", deadStateData, this);
        
        stateMachine.Init(IdleState);
    }


   
}
