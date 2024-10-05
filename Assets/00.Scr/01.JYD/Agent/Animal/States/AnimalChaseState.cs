using UnityEngine;

public class AnimalChaseState : AnimalState
{
    private AgentMovement agentMovement;
    private Transform target;
    
    
    public AnimalChaseState(Animal animal, AnimalStateMachine stateMachine, string animBoolHash) : base(animal, stateMachine, animBoolHash)
    {
        agentMovement = animal.GetCompo<AgentMovement>();
    }

    public override void Enter()
    {
        base.Enter();
        agentMovement.SetSpeed(Animal.chaseSpeed);
        target = Animal.player;
    }

    public override void Update()
    {
        base.Update();
        Animal.FactToTarget(agentMovement.GetNextPathPoint());
        
        float distance = Vector3.Distance(Animal.transform.position , target.position);
        if (distance <= Animal.attackRadius)
        {
            StateMachine.ChangeState(AnimalStateEnum.Attack);
            return;
        }
        
        agentMovement.SetDestination(target.position);
    }

    public override void Exit()
    {
        base.Exit();
    }
}