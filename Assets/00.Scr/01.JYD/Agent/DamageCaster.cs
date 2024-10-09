using System;
using UnityEngine;
using UnityEngine.Rendering;

public class DamageCaster : MonoBehaviour,IAgentComponent
{
    private Agent Agent;
    
    public Transform damageCasterTrm;
    public float radius;
    public LayerMask whatIsPlayer;

    private Collider[] target;

    public float damage;
    
    public void Initialize(Agent agent)
    {
        Agent = agent;
    }
    
    private void Start()
    {
        target = new Collider[1];
    }

    public void DamageCast()
    {
        target = Physics.OverlapSphere(damageCasterTrm.position, radius, whatIsPlayer);
        target[0].GetComponent<ExHealth>().GetDamage(damage);
    }
    
    
}