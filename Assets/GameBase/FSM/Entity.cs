using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LaniakeaCode.Utilities;

/// <summary>
/// Represents an entity in the game with core components and state management.
/// </summary>
public class Entity : MonoBehaviour
{
    // TODO Consider Adding A Nav Mesh Component To Manage Movement in Easier Way for Enemies
    /// <summary>
    /// Gets the core component of the entity.
    /// </summary>
    public CORE Core { get; private set; }

    /// <summary>
    /// Gets the animator component of the entity.
    /// </summary>
    public Animator Anim { get; private set; }

    /// <summary>
    /// Gets the attack FSM component of the entity.
    /// </summary>
    public AttackToFSM AttackFSM { get; private set; }

    /// <summary>
    /// Gets the animation to FSM component of the entity.
    /// </summary>
    public AnimToFSM AnimToFSM { get; private set; }

    /// <summary>
    /// The state machine managing the entity's states.
    /// </summary>
    public FSM stateMachine;

    /// <summary>
    /// Enables or disables state debugging.
    /// </summary>
    public bool EnableStateDebug;

    // This Is A generic container for the data.
    [Header("Data")]
    [SerializeField]
    [ExposedScriptableObject]
    private EntityData entityData;

    private float currentStunResistance;
    private float lastDamageTime;
    private float currentHealth;

    protected bool isStunned;
    protected bool isDead;

    /// <summary>
    /// Gets the direction of the last damage taken.
    /// </summary>
    public int LastDamageDirection { get; private set; }

    // A memory saver vector.
    // We use it in all our calculations, so we don't generate a new one each time.
    private Vector2 workVector;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Core = GetComponentInChildren<CORE>();
    }

    /// <summary>
    /// Called when the script is loaded or a value is changed in the inspector (Editor only).
    /// </summary>
    private void OnValidate()
    {
        Debug.Log($"<color=lightgreen>{nameof(Entity)} ({gameObject.name}): Validate called</color>");
    }

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
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

    /// <summary>
    /// Called once per frame.
    /// </summary>
    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();

        Anim.SetFloat("YVelocity", Core.movement.Rb.velocity.y);
        Anim.SetFloat("XVelocity", Mathf.Abs(Core.movement.Rb.velocity.x));

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            // ResetStunResistance();
        }
    }

    /// <summary>
    /// Called every fixed framerate frame.
    /// </summary>
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
}