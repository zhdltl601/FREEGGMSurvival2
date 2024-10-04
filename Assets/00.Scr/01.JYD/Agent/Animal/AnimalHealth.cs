using System;
using System.Linq;
using UnityEngine;

public class AnimalHealth : MonoBehaviour,IDamageAble,IAgentComponent
{
    private Animal _animal;
    private AgentMovement movement;
    
    
    public float health;

    public GameObject hitImpact;

    public event Action OnDeadEvent;
    public Transform test;
    
    public void Initialize(Agent agent)
    {
        _animal = agent as Animal;
        movement = _animal.GetCompo<AgentMovement>();
    }
    
    [ContextMenu("HitTest")]
    public void Test()
    {
        GetDamage(10 ,test.position , -test.position);
    }
    
    public void GetDamage(float damage , Vector3 hitPoint , Vector3 normal)
    {
        Hit(hitPoint , normal);
        movement.GetKnockBack(damage * hitPoint);
        
        
        
        if (health - damage <= 0)
        {
            Dead();
        }
    }

    public void Hit(Vector3 hitPoint, Vector3 normal)
    {
        GameObject newHit = Instantiate(hitImpact , hitPoint , Quaternion.LookRotation(normal));
        newHit.GetComponents<ParticleSystem>().ToList().ForEach(x =>
        {
            x.Simulate(0);
            x.Play();
        });
    }

    public void Dead()
    {
        OnDeadEvent?.Invoke();
        
        Debug.Log("죽음");
    }


   
}
