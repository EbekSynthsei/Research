// DoorClosedState.cs — stato di riposo, nessuna logica attiva
public class DoorClosedState : State
{
    public DoorClosedState(Entity entity, FSM stateMachine, string animBoolName)
        : base(entity, stateMachine, animBoolName) { }
}