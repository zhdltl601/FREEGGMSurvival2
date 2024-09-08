using UnityEngine;

public class AnimalIdleState : AnimalState
{
    private float timer = 0;
    private float stateTimer = 2;
    public AnimalIdleState(Animal animal, AnimalStateMachine stateMachine, string animBoolHash) : base(animal, stateMachine, animBoolHash)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        timer = 0;
    }

    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        
        if (stateTimer < timer)
        {
            StateMachine.ChangeState(AnimalStateEnum.Move);

        }
        
    }

    public override void Exit()
    {
        base.Exit();
      
    }
}