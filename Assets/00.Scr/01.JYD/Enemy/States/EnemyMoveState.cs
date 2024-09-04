using TMPro;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    private Vector3 targetDestination;
    private EnemyMovement movement;
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        movement = enemy.GetCompo<EnemyMovement>();
    }

    public override void Enter()
    {
        base.Enter();
        movement.SetSpeed(enemy.walkSpeed);
        
        SetDestination(movement.GetNextPatrolPoint());
    }

    public override void Update()
    {
        base.Update();
        enemy.FactToTarget(movement.GetNextPathPoint());

        if (movement.IsArrived)
        {
            stateMachine.ChangeState(EnemyStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
        movement.SetSpeed(0);
    }
    
    private void SetDestination(Vector3 position)
    {
        targetDestination = position;
        movement.SetDestination(targetDestination);
    }
}