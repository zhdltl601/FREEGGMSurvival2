using UnityEngine;
public class EnemyChaseState : EnemyState
{
    private EnemyMovement movement;
    private EnemyCombatSystem combatSystem;
    public EnemyChaseState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName) : base(_enemy, _stateMachine, animBoolName)
    {
        movement = enemy.GetCompo<EnemyMovement>();
        combatSystem = enemy.GetCompo<EnemyCombatSystem>();
    }

    public override void Enter()
    {
        base.Enter();
        movement.SetSpeed(enemy.runningSpeed);
    }

    public override void Exit()
    {
        base.Exit();
        movement.SetSpeed(0);
    }

    public override void Update()
    {
        base.Update();

        if (combatSystem.IsAttackAble())
        {
            stateMachine.ChangeState(EnemyStateEnum.Attack);
        }
        
        enemy.FactToTarget(movement.GetNextPathPoint());
        movement.SetDestination(enemy.targetTrm.position);
    }
}