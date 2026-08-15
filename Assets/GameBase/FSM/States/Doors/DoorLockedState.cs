// DoorLockedState.cs — feedback breve, poi torna a Closed
using UnityEngine;

public class DoorLockedState : State
{
    private readonly Door door;
    private const float FeedbackDuration = 0.6f;

    public DoorLockedState(Entity entity, FSM stateMachine, string animBoolName)
        : base(entity, stateMachine, animBoolName)
    {
        door = (Door)entity;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log($"{entity.name}: porta bloccata, serve la key '{door.RequiredKeyId}'.", entity);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= StartTime + FeedbackDuration)
        {
            stateMachine.ChangeState(door.ClosedState);
        }
    }
}