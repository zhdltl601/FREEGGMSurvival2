using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum EnemyStateEnum
{
    Idle,
    Move,
    Chase,
    Recovery,
    Attack,
}

public class Enemy : Agent
{
    private EnemyStateMachine stateMachine;
    
    [Header("Target")]
    public Transform targetTrm;
        
    [Header("Move info")] 
    public float runningSpeed;
    public float walkSpeed;
    private int currentWayPoint;

    [Header("Attack info")] 
    public float aggressiveRange;
    public float attackAbleRange;
    public bool isBattleMode;
    
    public LayerMask whatIsPlayer;
    public LayerMask whatIsObstacle;


    private readonly Collider[] _enemyCheckCollider = new Collider[1];
    
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        
        #region StateAdd
        
        stateMachine.AddDictionary(EnemyStateEnum.Idle , new EnemyIdleState(this , stateMachine , "Idle"));
        stateMachine.AddDictionary(EnemyStateEnum.Move , new EnemyMoveState(this , stateMachine , "Move"));
        stateMachine.AddDictionary(EnemyStateEnum.Attack , new EnemyAttackState(this , stateMachine , "Attack"));
        stateMachine.AddDictionary(EnemyStateEnum.Chase , new EnemyChaseState(this , stateMachine , "Chase"));
        stateMachine.AddDictionary(EnemyStateEnum.Recovery , new EnemyRecoveryState(this , stateMachine , "Recovery"));

        #endregion
    }
        
    private void Start()
    {
        stateMachine.Initialize(EnemyStateEnum.Idle);
        
        GetCompo<AgentMovement>().SetSpeed(walkSpeed);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        
        EnterBattleMode();
    }

    /*public virtual Collider IsPlayerDetected()
    {
        int cnt = Physics.OverlapSphereNonAlloc(transform.position, chaseCheckDistance, _enemyCheckCollider, whatIsPlayer);
        return cnt == 1 ? _enemyCheckCollider[0] : null;
    }
    
    public virtual bool IsObstacleInLine(float distance, Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, distance,whatIsObstacle);
    }*/
    
    private bool IsPlayerInAggressiveRange()
    {
        Vector3 pos = transform.position;
        return Vector3.Distance(pos , targetTrm.position) <= aggressiveRange;
    }
    
    private void EnterBattleMode()
    {
        if (IsPlayerInAggressiveRange() && isBattleMode == false)
        {
            isBattleMode = true;
            GetCompo<EnemyAnimator>().SetupRecovery(0);//맨 처음은 무조건0이죠.. ㅋㅋ
            stateMachine.ChangeState(EnemyStateEnum.Recovery);
        }
    }
    
    public void AnimationEnd()
    {
        stateMachine.currentState.AnimationTriggerCalled();
    }
    
}