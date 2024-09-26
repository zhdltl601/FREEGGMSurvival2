using UnityEngine;

public class EnemyRecoveryState : EnemyState
{
    private AgentMovement movement;
    private EnemyCombatSystem combatSystem;
    public EnemyRecoveryState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName) : base(_enemy, _stateMachine, animBoolName)
    {
        movement = _enemy.GetComponent<AgentMovement>();
        combatSystem = _enemy.GetComponent<EnemyCombatSystem>();
    }

    public override void Enter()
    {
        base.Enter();
        movement.SetStopped(true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.GetCompo<EnemyAnimator>().SetupRecovery(1);
        
        movement.SetStopped(false);
    }

    public override void Update()
    {
        base.Update();
        enemy.FactToTarget(enemy.targetTrm.position);
        
        if (animationTriggerd)
        {
            var nextState = combatSystem.IsAttackAble() ? EnemyStateEnum.Attack : EnemyStateEnum.Chase;
            stateMachine.ChangeState(nextState);
            Debug.Log(nextState);
        }
    }
} 