using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExResourceHealth : ExHealth
{
    public override void GetDamage(float damage, Vector3 hitPoint, Vector3 normal)
    {
        //전용도구가 아니면 노데미지밍..
        //if()
        base.GetDamage(damage, hitPoint, normal);
    }

}
