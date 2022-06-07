using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LaniakeaCode.Utilities;
public class Entity : MonoBehaviour
{
    //Main Components. Consider Adding A Nav Mesh Component To Manage Movement in Easier Way for Enemies
    public CORE Core { get; private set; }
    public Animator Anim { get; private set; }
    public AttackToFSM AttackFSM { get; private set; }
    public AnimToFSM AnimToFSM { get; private set; }


    public FSM stateMachine;
    public bool EnableStateDebug;

    
    //This Is A generic container for the data.
    [Header("Data")]
    [SerializeField]
    [ExposedScriptableObject]
    private EntityData entityData;


    private float currentStunResistance;
    private float lastDamageTime;
    private float currentHealth;

    protected bool isStunned;
    protected bool isDead;
    public int LastDamageDirection { get; private set; }

    //A memory saver vector.
    //We use it in all our calculations, so we don't generate a new one each time.
    private Vector2 workVector;
    private void Awake()
    {
        Core = GetComponentInChildren<CORE>();
    }
    public virtual void Start()
    {
        currentHealth = entityData.maxHealth;
        stateMachine = new FSM();
        
        Anim = transform.GetComponent<Animator>();
        AnimToFSM = transform.GetComponent<AnimToFSM>();
        AttackFSM = transform.GetComponent<AttackToFSM>();

        currentStunResistance = entityData.stunResistance;
        isDead = false;

    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();

        Anim.SetFloat("YVelocity", Core.movement.Rb.velocity.y);
        Anim.SetFloat("XVelocity", Mathf.Abs(Core.movement.Rb.velocity.x));

        if(Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            //ResetStunResistance();
        }
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    
}