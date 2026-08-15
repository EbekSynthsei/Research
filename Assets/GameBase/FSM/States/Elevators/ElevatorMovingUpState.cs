using UnityEngine;

public class ElevatorMovingUpState : State
{
    private readonly Elevator elevator;

    public ElevatorMovingUpState(Entity entity, FSM stateMachine, string animBoolName)
        : base(entity, stateMachine, animBoolName)
    {
        elevator = (Elevator)entity;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        elevator.transform.position = Vector3.MoveTowards(
            elevator.transform.position, elevator.TargetUp.position, elevator.Speed * Time.fixedDeltaTime);

        if (Vector3.Distance(elevator.transform.position, elevator.TargetUp.position) < 0.01f)
        {
            stateMachine.ChangeState(elevator.ArrivedState);
        }
    }

    public override bool CanBeInterrupted() => false;
}