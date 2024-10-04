using UnityEngine;

public class AnimalAttackState : AnimalState
{
    private AgentMovement _agentMovement;
    private Vector3 _startPosition;
    public AnimalAttackState(Animal animal, AnimalStateMachine stateMachine, string animBoolHash) : base(animal, stateMachine, animBoolHash)
    {
        _agentMovement = animal.GetCompo<AgentMovement>();
    }

    public override void Enter()
    {
        base.Enter();
        _agentMovement.SetSpeed(0);
        _agentMovement.SetStopped(true);
        
    }

    public override void Update()
    {
        base.Update();
        
        if (_agentMovement.IsManualRotation)
        {
            Animal.FactToTarget(Animal.player.transform.position);
        }
        
        if (animationTriggerdCalled)
        {
            StateMachine.ChangeState(AnimalStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _agentMovement.SetStopped(false);
    }
}