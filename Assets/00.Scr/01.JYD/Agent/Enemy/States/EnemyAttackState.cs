using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private readonly int AtkIndex = Animator.StringToHash("AtkIndex");
    
    private AgentMovement movement;
    private EnemyAnimator enemyAnimator;

    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName) : base(_enemy, _stateMachine, animBoolName)
    {
        movement = _enemy.GetCompo<AgentMovement>();
        enemyAnimator = _enemy.GetCompo<EnemyAnimator>();
    }

    public override void Enter()
    {
        base.Enter();
        int randomAtkIndex = Random.Range(0, 3);
        
        enemyAnimator.animator.SetInteger(AtkIndex,randomAtkIndex);
        movement.SetStopped(true);
                
    }

    public override void Exit()
    {
        base.Exit();
        movement.SetStopped(false);
    }

    public override void Update()
    {
        base.Update();
        
        if (animationTriggerd)
        {
            stateMachine.ChangeState(EnemyStateEnum.Recovery);
        }
    }
}