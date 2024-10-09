using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRecoveryState : AnimalState
{
    private float timer;
    private AgentMovement _agentMovement;
    public AnimalRecoveryState(Animal animal, AnimalStateMachine stateMachine, string animBoolHash) : base(animal, stateMachine, animBoolHash)
    {
        _agentMovement = animal.GetCompo<AgentMovement>();
    }
    
    public override void Enter()
    {
        base.Enter();
        _agentMovement.NavMeshAgent.velocity = Vector3.zero;
        _agentMovement.SetStopped(true);
        timer = 0;
    }

    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if (timer > Animal.attackCooldown)
        {
            float distance = Vector3.Distance(Animal.transform.position , Animal.player.transform.position);
            var nextState = distance < Animal.attackRadius ? AnimalStateEnum.Attack : AnimalStateEnum.Chase;    
            StateMachine.ChangeState(nextState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _agentMovement.SetStopped(false);
    }
}
