using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExPlayerHealth : ExHealth
{
    public float Armor=0;
    public override void GetDamage(float damage, Vector3 hitPoint, Vector3 normal)
    {
        damage /= 1+Armor;
        base.GetDamage(damage, hitPoint, normal);
    }

    public override void Dead()
    {
        //À¸¾Ç¹Î ³ª µÚÁü
        OnDeadEvent?.Invoke();
    }
}
