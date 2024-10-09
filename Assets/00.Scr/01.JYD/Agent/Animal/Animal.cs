using System;
using UnityEngine;

public enum AnimalStateEnum
{
    Idle,
    Move,
    Chase,
    Attack,
    Recovery,
}

public class Animal : Agent
{
    private AnimalStateMachine StateMachine;
    
    public LayerMask whatIsPlayer;
    public Transform player;
    private Collider[] cols;

    public float walkSpeed;
    
    [Header("Attack info")] 
    public bool isBattleMode;
    public float chaseSpeed = 8f;
    public float chaseRadius;
    public float attackRadius = 1.2f;
    public float attackCooldown = 0.8f;
    private DamageCaster _damageCaster;

    private ExHealth _damageable;
    
    protected override void Awake()
    {
        base.Awake();
        
        StateMachine = new AnimalStateMachine();
        StateMachine.AddState(AnimalStateEnum.Idle , new AnimalIdleState(this , StateMachine , "Idle"));
        StateMachine.AddState(AnimalStateEnum.Move , new AnimalMoveState(this , StateMachine , "Move"));
        StateMachine.AddState(AnimalStateEnum.Chase, new AnimalChaseState(this , StateMachine , "Move"));
        StateMachine.AddState(AnimalStateEnum.Attack , new AnimalAttackState(this , StateMachine , "Attack"));
        StateMachine.AddState(AnimalStateEnum.Recovery , new AnimalRecoveryState(this , StateMachine , "Idle"));
    }

    private void Start()
    {
        StateMachine.Initialize(AnimalStateEnum.Idle);
        _damageCaster = GetCompo<DamageCaster>();
        _damageable = GetComponent<ExHealth>();
        _damageable.OnHitEvent += EnteringBattleMode;
        
        cols = new Collider[1];
    }

    private void Update()
    {
        StateMachine.currentState.Update();
        
        //print(StateMachine.currentState);
        
    }
    
    public void AnimationEnd()
    {
        StateMachine.currentState.AnimationTrigger();
    }

    
    private void EnteringBattleMode()
    {
        if(isBattleMode)return;
        
        isBattleMode = true;
        
        int cnt = Physics.OverlapSphereNonAlloc(transform.position ,chaseRadius ,  cols , whatIsPlayer);

        if (cnt > 0)
        {
            player = cols[0].transform;
            //GetCompo<AgentMovement>().GetKnockBack(-transform.forward);
            StateMachine.ChangeState(AnimalStateEnum.Recovery);
        }
    }
    
    //animationEvent
    public override void DamageCast()
    {
        _damageCaster.DamageCast();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position , attackRadius);
    }
}
