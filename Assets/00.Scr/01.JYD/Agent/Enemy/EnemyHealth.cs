using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IDamageAble,IAgentComponent
{
    private Agent _agent;

    public float health;
    
    public void Initialize(Agent agent)
    {
        _agent = agent;
    }
    
    public void GetDamage(float damage, Vector3 hitPoint, Vector3 normal)
    {
        health -= damage;
    }

    public void Hit(Vector3 hitPoint, Vector3 normal)
    {
        
    }

    public void Dead()
    {
        
    }
}
