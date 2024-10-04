using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : Damageable
{
    private void OnTriggerEnter(Collider other)
    {
        ApplyDamage(other.transform,other.transform.position,other.transform.position - transform.position);
    }
}
