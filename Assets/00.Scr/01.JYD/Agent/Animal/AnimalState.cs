using UnityEngine;

public class AnimalState
{

    protected Animal Animal;
    protected AnimalStateMachine StateMachine;
    protected int AnimBoolHash;

    protected Animator animator;

    protected bool animationTriggerdCalled;
        
    public AnimalState(Animal animal, AnimalStateMachine stateMachine, string animBoolHash)
    {
        Animal = animal;
        StateMachine = stateMachine;
        AnimBoolHash = Animator.StringToHash(animBoolHash);
                
        animator = Animal.GetCompo<AnimalAnimationControls>().Animator;
    }
    
    public virtual void Enter()
    {
        animator.SetBool(AnimBoolHash , true);
        animationTriggerdCalled = false;
    }

    public virtual void Update()
    {
        
    }
    public virtual void Exit()
    {
        animator.SetBool(AnimBoolHash , false);
    }

    public void AnimationTrigger()
    {
        animationTriggerdCalled = true;
    }
}
