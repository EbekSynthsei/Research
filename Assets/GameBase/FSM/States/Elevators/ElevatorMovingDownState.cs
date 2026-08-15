using UnityEngine;

public class ElevatorMovingDownState : State
{
    private readonly Elevator elevator;

    public ElevatorMovingDownState(Entity entity, FSM stateMachine, string animBoolName)
        : base(entity, stateMachine, animBoolName)
    {
        elevator = (Elevator)entity;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        elevator.transform.position = Vector3.MoveTowards(
            elevator.transform.position, elevator.TargetDown.position, elevator.Speed * Time.fixedDeltaTime);

        if (Vector3.Distance(elevator.transform.position, elevator.TargetDown.position) < 0.01f)
        {
            stateMachine.ChangeState(elevator.ArrivedState);
        }
    }

    public override bool CanBeInterrupted() => false;
}