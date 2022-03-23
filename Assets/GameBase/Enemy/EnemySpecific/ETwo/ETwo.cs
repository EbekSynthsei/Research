using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETwo : Entity
{

    #region States

    public ETwoIdleState IdleState { get; private set; }
    public ETwoMoveState MoveState { get; private set; }

    public ETwoTargetDetectedState TargetDetectedState { get; private set; }
    public ETwoSearchTargetState SearchTargetState { get; private set; }

    public ETwoMeleeAttackState MeleeAttackState { get; private set; }
    public ETwoRangedAttackState RangedAttackState { get; private set; }

    public ETwoDodgeState DodgeState { get; private set; }


    public ETwoStunState StunState { get; private set; }
    public ETwoDeadState DeadState { get; private set; }
    #endregion

    #region State Data
    [Header("State Data")]
    [SerializeField]
    private IdleStateData idleStateData;
    [SerializeField]
    private MoveStateData moveStateData;

    [Space]

    [SerializeField]
    private TargetDetectedStateData targetDetectedStateData;
    [SerializeField]
    private SearchTargetData searchTargetData;

    [Header("CombatStateData")]
    [SerializeField]
    private MeleeAttackData meleeAttackStateData;
    [SerializeField]
    private RangedAttackData rangedAttackStateData;
     
    [Space]

    [SerializeField]
    public DodgeStateData dodgeStateData;

    [Header("Damage Data")]
    [SerializeField]
    private StunStateData stunStateData;
    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangedAttackPosition;
    [SerializeField]
    private DeadStateData deadStateData;
    #endregion
    public override void Start()
    {
        base.Start();

        //MOVE
        MoveState = new ETwoMoveState(this, stateMachine, "Move", moveStateData, this);
        IdleState = new ETwoIdleState(this, stateMachine, "Idle", idleStateData, this);

        //SearchAndAttack
        TargetDetectedState = new ETwoTargetDetectedState(this, stateMachine, "Detecting", targetDetectedStateData, this);
        SearchTargetState = new ETwoSearchTargetState(this, stateMachine, "Searching", searchTargetData, this);
        
        MeleeAttackState = new ETwoMeleeAttackState(this, stateMachine, "MeleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        RangedAttackState = new ETwoRangedAttackState(this, stateMachine, "RangedAttack", rangedAttackPosition, rangedAttackStateData, this);
        
        DodgeState = new ETwoDodgeState(this, stateMachine, "Dodging", dodgeStateData, this);
        //Damage
        StunState = new ETwoStunState(this, stateMachine, "Stunned", stunStateData, this);
        DeadState = new ETwoDeadState(this, stateMachine, "Dead", deadStateData, this);

        stateMachine.Init(IdleState);
    }

    
   
}
        
