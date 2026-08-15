// DoorOpeningState.cs — transizione animata, avanza a Open a fine clip
public class DoorOpeningState : State
{
    private readonly Door door;

    public DoorOpeningState(Entity entity, FSM stateMachine, string animBoolName)
        : base(entity, stateMachine, animBoolName)
    {
        door = (Door)entity;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        stateMachine.ChangeState(door.OpenState);
    }

    public override bool CanBeInterrupted() => false; // non interrompibile a metà apertura
}