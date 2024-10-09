using UnityEngine;
using UnityEngine.Serialization;

public class AnimalAnimationControls : MonoBehaviour,IAgentComponent
{
    public Animator Animator;
    public Animal animal;

    private AgentMovement agentMovement;
    
    public void Initialize(Agent agent)
    {
        animal = agent as Animal;
        Animator = GetComponent<Animator>();
        agentMovement = animal.GetCompo<AgentMovement>();
    }
    public void AnimationEnd()
    {
        animal.AnimationEnd();
    }

    public void StartManualRotation()
    {
        agentMovement.SetManualRotate(true);
    }

    public void StopManualRotation()
    {
        agentMovement.SetManualRotate(false);
    }
    
    public void DamageCast()
    {
        animal.DamageCast();
    }

    public void PlayAttackSFX()
    {
        animal.AttackSFXPlay();
        
    }
    
}