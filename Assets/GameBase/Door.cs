using UnityEngine;

/// <summary>
/// Porta interattiva con FSM propria (Closed/Opening/Open/Closing/Locked).
/// Eredita Entity per riusare Anim/AnimToFSM/State/FSM, ma NON usa Core/movement:
/// Update()/FixedUpdate() sono overridati per evitare NRE su Core nullo.
/// </summary>
public class Door : Entity, IActivatable
{
    public DoorClosedState ClosedState { get; private set; }
    public DoorOpeningState OpeningState { get; private set; }
    public DoorOpenState OpenState { get; private set; }
    public DoorClosingState ClosingState { get; private set; }
    public DoorLockedState LockedState { get; private set; }

    [Header("Door Settings")]
    [SerializeField] private bool requiresKey;
    [SerializeField] private string requiredKeyId;
    [SerializeField] private bool startOpen;

    public bool RequiresKey => requiresKey;
    public string RequiredKeyId => requiredKeyId;

    public override void Start()
    {
        base.Start(); // FSM, Anim, AnimToFSM wiring (richiede EntityData assegnato in Inspector)

        ClosedState = new DoorClosedState(this, stateMachine, "Closed");
        OpeningState = new DoorOpeningState(this, stateMachine, "Opening");
        OpenState = new DoorOpenState(this, stateMachine, "Open");
        ClosingState = new DoorClosingState(this, stateMachine, "Closing");
        LockedState = new DoorLockedState(this, stateMachine, "Locked");

        stateMachine.Init(startOpen ? (State)OpenState : ClosedState);
    }

    // Override completo: niente base.Update(), Door non ha CORE.
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
        if (requiresKey && !HasRequiredKey(instigator))
        {
            stateMachine.ChangeState(LockedState);
            return;
        }

        if (stateMachine.currentState == ClosedState)
        {
            stateMachine.ChangeState(OpeningState);
        }
        else if (stateMachine.currentState == OpenState)
        {
            stateMachine.ChangeState(ClosingState);
        }
        // Se è già in Opening/Closing/Locked, ignora input ripetuti (debounce naturale).
    }

    private bool HasRequiredKey(Entity instigator)
    {
        // TODO: PlayerInventory non ha ancora un sistema di key/item generico.
        // Placeholder: da collegare quando estendi PlayerInventory oltre alle sole armi.
        if (instigator is Player player && player.Inventory != null)
        {
            // return player.Inventory.HasKey(requiredKeyId);
        }
        return false;
    }
}