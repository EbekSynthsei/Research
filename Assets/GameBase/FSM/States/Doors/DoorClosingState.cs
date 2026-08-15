// DoorClosingState.cs — simmetrico a Opening
public class DoorClosingState : State
{
    private readonly Door door;

    public DoorClosingState(Entity entity, FSM stateMachine, string animBoolName)
        : base(entity, stateMachine, animBoolName)
    {
        door = (Door)entity;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        stateMachine.ChangeState(door.ClosedState);
    }

    public override bool CanBeInterrupted() => false;
}