public class AnimalEatState : AnimalState
{
    public AnimalEatState(Animal animal, AnimalStateMachine stateMachine, string animBoolHash) : base(animal, stateMachine, animBoolHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Animal.GetCompo<AgentMovement>().SetDestination(Animal.eatingTarget.position);
    }

    public override void Update()
    {
        base.Update();
        if (animationTriggerdCalled)
        {
            StateMachine.ChangeState(AnimalStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Animal.isEatingMode = false;
    }
}