using UnityEngine;

public class ElevatorMovingState : State
{
    private readonly Elevator elevator;
    private readonly Vector3 targetPosition;

    public ElevatorMovingState(Entity entity, FSM stateMachine, string animBoolName, Vector3 target)
        : base(entity, stateMachine, animBoolName)
    {
        elevator = (Elevator)entity;
        targetPosition = target;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        elevator.transform.position = Vector3.MoveTowards(
            elevator.transform.position, targetPosition, elevator.Speed * Time.fixedDeltaTime);

        if (Vector3.Distance(elevator.transform.position, targetPosition) < 0.01f)
        {
            stateMachine.ChangeState(elevator.ArrivedState);
        }
    }

    public override bool CanBeInterrupted() => false;
}