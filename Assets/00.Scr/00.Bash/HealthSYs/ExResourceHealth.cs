using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExResourceHealth : ExHealth
{
    public override void GetDamage(float damage, Vector3 hitPoint, Vector3 normal)
    {
        //���뵵���� �ƴϸ� �뵥������..
        //if()
        base.GetDamage(damage, hitPoint, normal);
    }

}
