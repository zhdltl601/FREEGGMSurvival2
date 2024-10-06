using UnityEngine;

public class AnimalMoveState : AnimalState
{
    private AgentMovement movement;
    
    public AnimalMoveState(Animal animal, AnimalStateMachine stateMachine, string animBoolHash) : base(animal, stateMachine, animBoolHash)
    {
        movement = Animal.GetCompo<AgentMovement>();
    }

    public override void Enter()
    {
        base.Enter();
        movement.SetSpeed(1.5f);

        Vector3 target = movement.GetNextPatrolPoint();
        
        movement.SetDestination(target);
    }

    public override void Update()
    {
        base.Update();
        Animal.FactToTarget(movement.GetNextPathPoint());

        if (movement.IsArrived)
        {
            AnimalStateEnum nextState = AnimalStateEnum.Idle;
            StateMachine.ChangeState(nextState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}