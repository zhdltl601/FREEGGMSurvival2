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
        
        if (Animal.isEatingMode)
        {
            target = Animal.eatingTarget.position;
        }
        
        movement.SetDestination(target);
    }

    public override void Update()
    {
        base.Update();
        Animal.FactToTarget(movement.GetNextPathPoint());

        if (movement.IsArrived)
        {
            AnimalStateEnum nextState = AnimalStateEnum.Idle;
            if (Animal.isEatingMode)
            {
                nextState = AnimalStateEnum.Eat;
            }

            StateMachine.ChangeState(nextState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}