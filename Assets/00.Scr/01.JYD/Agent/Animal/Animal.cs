

using UnityEngine;

public enum AnimalStateEnum
{
    Idle,
    Move,
    Eat,
    Sleep,
    Hostile,
}

public class Animal : Agent
{
    private AnimalStateMachine StateMachine;

    public bool isEatingMode;
    public LayerMask whatIsEating;
    public float eatRadius;
    public Transform eatingTarget;
    private Collider[] cols;
    
    
    private void Awake()
    {
        base.Awake();
        
        StateMachine = new AnimalStateMachine();
        StateMachine.AddState(AnimalStateEnum.Idle , new AnimalIdleState(this , StateMachine , "Idle"));
        StateMachine.AddState(AnimalStateEnum.Move , new AnimalMoveState(this , StateMachine , "Move"));
        StateMachine.AddState(AnimalStateEnum.Eat , new AnimalEatState(this , StateMachine , "Eat"));
        
    }

    private void Start()
    {
        StateMachine.Initialize(AnimalStateEnum.Idle);

        cols = new Collider[1];
    }

    private void Update()
    {
        StateMachine.currentState.Update();
        EnteringAppetiteMode();
    }
    
    public void AnimationEnd()
    {
        StateMachine.currentState.AnimationTrigger();
    }

    private void EnteringAppetiteMode()
    {
        int cnt = Physics.OverlapSphereNonAlloc(transform.position ,eatRadius ,  cols , whatIsEating);
        if (cnt > 0)
        {
            isEatingMode = true;
            eatingTarget = cols[0].transform;
            StateMachine.ChangeState(AnimalStateEnum.Eat);
        }        
    }
}
