public class AnimalAttackState : AnimalState
{
    
    public AnimalAttackState(Animal animal, AnimalStateMachine stateMachine, string animBoolHash) : base(animal, stateMachine, animBoolHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
    }
}