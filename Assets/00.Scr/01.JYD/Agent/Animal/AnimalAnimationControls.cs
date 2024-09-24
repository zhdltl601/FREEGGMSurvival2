using UnityEngine;
using UnityEngine.Serialization;

public class AnimalAnimationControls : MonoBehaviour,IAgentComponent
{
    public Animator Animator;
    public Animal _animal;
    
    public void Initialize(Agent agent)
    {
        _animal = agent as Animal;
        Animator = GetComponent<Animator>();

    }
    public void AnimationEnd()
    {
        _animal.AnimationEnd();
    }

    
}