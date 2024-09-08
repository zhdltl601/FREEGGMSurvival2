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
        movement.SetDestination(movement.GetNextPatrolPoint());
    }

    public override void Update()
    {
        base.Update();
        Animal.FactToTarget(movement.GetNextPathPoint());

        if (movement.IsArrived)
        {
            StateMachine.ChangeState(AnimalStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}