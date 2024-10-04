using UnityEngine;

public enum AnimalStateEnum
{
    Idle,
    Move,
    Eat,
    Chase,
    Attack,
    Sleep,
    Hostile,
}

public class Animal : Agent
{
    private AnimalStateMachine StateMachine;
    
    public bool isEatingMode;
    
    public LayerMask whatIsEating,whatIsPlayer;
    public float eatRadius;
    public Transform eatingTarget,player;
    private Collider[] cols;

    [Header("Attack info")] 
    public bool isBattleMode;
    public float chaseSpeed = 8f;
    public float chaseRadius;
    public float attackRadius = 1.2f;
    public float attackCooldown = 0.8f;
    private DamageCaster _damageCaster;
    
    protected override void Awake()
    {
        base.Awake();
        
        StateMachine = new AnimalStateMachine();
        StateMachine.AddState(AnimalStateEnum.Idle , new AnimalIdleState(this , StateMachine , "Idle"));
        StateMachine.AddState(AnimalStateEnum.Move , new AnimalMoveState(this , StateMachine , "Move"));
        StateMachine.AddState(AnimalStateEnum.Eat , new AnimalEatState(this , StateMachine , "Eat"));
        StateMachine.AddState(AnimalStateEnum.Chase, new AnimalChaseState(this , StateMachine , "Move"));
        StateMachine.AddState(AnimalStateEnum.Attack , new AnimalAttackState(this , StateMachine , "Attack"));
        
    }

    private void Start()
    {
        StateMachine.Initialize(AnimalStateEnum.Idle);
        _damageCaster = GetCompo<DamageCaster>();
        
        cols = new Collider[1];
    }

    private void Update()
    {
        StateMachine.currentState.Update();
        EnteringEatMode();
    }
    
    public void AnimationEnd()
    {
        StateMachine.currentState.AnimationTrigger();
    }

    private void EnteringEatMode()
    {
        if(isBattleMode)return;
        
        int cnt = Physics.OverlapSphereNonAlloc(transform.position ,eatRadius ,  cols , whatIsEating);
        if (cnt > 0)
        {
            isEatingMode = true;
            eatingTarget = cols[0].transform;
            StateMachine.ChangeState(AnimalStateEnum.Eat);
        }        
    }
    
    [ContextMenu("Test")]
    private void EnteringBattleMode()
    {
        isBattleMode = true;
        
        int cnt = Physics.OverlapSphereNonAlloc(transform.position ,chaseRadius ,  cols , whatIsPlayer);

        if (cnt > 0)
        {
            player = cols[0].transform;
            var nextState = Vector3.Distance(transform.position, player.position) < attackRadius ? 
                AnimalStateEnum.Attack : AnimalStateEnum.Chase; 
            StateMachine.ChangeState(nextState);
        }
    }
    
    //animationEvent
    public override void DamageCast()
    {
        _damageCaster.DamageCast();
    }
}
