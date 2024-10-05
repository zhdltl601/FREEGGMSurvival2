using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDamager : Damageable
{
    public LayerMask _whatIsTarget;
    public bool Scan(Vector3 origin, Vector3 direction,float distance)
    {
       if( Physics.Raycast(transform.position, transform.forward,out RaycastHit hit,distance,_whatIsTarget))
        {
            ApplyDamage(hit.transform, hit.point, hit.normal);
        return true; 
        }
       return false;
    }
    public bool Scan(Vector3 origin, Vector3 direction, float distance,float radius)
    {
        if (Physics.SphereCast(transform.position,radius, transform.forward, out RaycastHit hit, distance, _whatIsTarget))
        {
            print(hit.collider.gameObject.name);
            ApplyDamage(hit.transform, hit.point, hit.normal);
            return true;
        }
        return false;
    }
}
