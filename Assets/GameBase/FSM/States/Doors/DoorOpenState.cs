// DoorOpenState.cs — porta aperta, in attesa di essere richiusa
public class DoorOpenState : State
{
    public DoorOpenState(Entity entity, FSM stateMachine, string animBoolName)
        : base(entity, stateMachine, animBoolName) { }
}