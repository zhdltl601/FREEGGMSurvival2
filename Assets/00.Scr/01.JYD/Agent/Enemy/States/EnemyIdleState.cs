using TMPro;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float stateTimer = 0.5f;
    private float timer = 0;
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        
    }
    
    public override void Update()
    {
        /*Collider player = enemy.IsPlayerDetected();
        if (player != null)
        {
            Vector3 direction = player.transform.position - enemy.transform.position;
            
            if (enemy.IsObstacleInLine(Mathf.Infinity, direction) == false)
            {
                enemy.targetTrm = player.transform;
                stateMachine.ChangeState(EnemyStateEnum.Move);
            }
        }*/

        timer += Time.deltaTime;
        if (timer > stateTimer)
        {
            stateMachine.ChangeState(EnemyStateEnum.Move);
        }
    }

    public override void Exit()
    {
        base.Exit();
        timer = 0;
    }
}