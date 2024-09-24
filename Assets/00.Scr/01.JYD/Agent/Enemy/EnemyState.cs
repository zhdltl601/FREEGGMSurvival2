using UnityEngine;
using UnityEngine.AI;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected int animBoolHash;

    protected bool animationTriggerd;
    
    protected Animator animator;
    
    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine,string animBoolName)
    {
        enemy = _enemy;
        stateMachine = _stateMachine;
        animBoolHash = Animator.StringToHash(animBoolName);

        animator = _enemy.GetCompo<EnemyAnimator>().animator;
        
    }
    
    public virtual void Enter()
    {
        animator.SetBool(animBoolHash , true);
        animationTriggerd = false;
    }
    
    public virtual void Exit()
    {
        animator.SetBool(animBoolHash , false);
    }
    
    public virtual void Update()
    {
        
    }

    public void AnimationTriggerCalled()
    {
        animationTriggerd = true;
    }
    
}
