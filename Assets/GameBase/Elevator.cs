using UnityEngine;

/// <summary>
/// Ascensore interattivo con FSM propria (MovingUp/MovingDown/Arrived).
/// Eredita Entity per riusare Anim/AnimToFSM/State/FSM, ma NON usa Core/movement:
/// Update()/FixedUpdate() sono overridati per evitare NRE su Core nullo.
/// </summary>
public class Elevator : Entity, IActivatable
{
    public ElevatorMovingUpState MovingUpState { get; private set; }
    public ElevatorMovingDownState MovingDownState { get; private set; }
    public ElevatorArrivedState ArrivedState { get; private set; }

    [Header("Elevator Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform targetUp;
    [SerializeField] private Transform targetDown;

    public float Speed => speed;
    public Transform TargetUp => targetUp;
    public Transform TargetDown => targetDown;
    
    public override void Start()
    {
        base.Start(); // FSM, Anim, AnimToFSM wiring (richiede EntityData assegnato in Inspector)

        MovingUpState = new ElevatorMovingUpState(this, stateMachine, "MovingUp");
        MovingDownState = new ElevatorMovingDownState(this, stateMachine, "MovingDown");
        ArrivedState = new ElevatorArrivedState(this, stateMachine, "Arrived");

        stateMachine.Init(ArrivedState);
    }

    // Override completo: niente base.Update(), Elevator non ha CORE.
    public override void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public override void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public void Activate(Entity instigator)
    {
        // Implementation of elevator activation logic
        if (stateMachine.currentState == ArrivedState)
        {
            // Simple logic: move to the opposite direction from where we are
            if (transform.position.y < targetUp.position.y)
            {
                stateMachine.ChangeState(MovingUpState);
            }
            else
            {
                stateMachine.ChangeState(MovingDownState);
            }
        }
    }
}