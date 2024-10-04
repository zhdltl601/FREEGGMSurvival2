using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExAnimalHealth : ExHealth
{
    private Animal _animal;
    public AgentMovement movement;
    public override void Initialize(Agent agent)
    {
        _animal = agent as Animal;
        movement = _animal.GetCompo<AgentMovement>();
    }

    public override void GetDamage(float damage, Vector3 hitPoint, Vector3 normal)
    {
        base.GetDamage(damage, hitPoint, normal);
        movement.GetKnockBack(damage * hitPoint);
    }
    public override void Hit(Vector3 hitPoint, Vector3 normal)
    {
        //ºÐ³ë¹Ö ¤§¤§
        base.Hit(hitPoint, normal);
    }
}
