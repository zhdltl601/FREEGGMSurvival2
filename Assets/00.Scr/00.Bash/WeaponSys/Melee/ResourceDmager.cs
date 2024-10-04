using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDmager : Damageable
{
    public ResourceType resourceType;
    private void OnTriggerEnter(Collider other)
    {
        ExResourceHealth healthCompo = GetResourceHealthCompo(other.transform);
        if (healthCompo)
        {
            healthCompo.GetResourceDamge(resourceType,_damage * 2,other.transform.position, other.transform.position - transform.position);
        }
    }
    protected virtual ExResourceHealth GetResourceHealthCompo(Transform target)
    {
        if (target.transform.gameObject.CompareTag("Hitable") || target.transform.gameObject.CompareTag("Critical")) //태그가 Hitable이거나 Critical일 떄.
        {
            Transform trmTmp = target.transform.gameObject.transform;
            ExResourceHealth healthCompo;

            while (!trmTmp.TryGetComponent<ExResourceHealth>(out healthCompo))
            {
                if (trmTmp.parent == null)
                {
                    Debug.LogError("NotHitableObj. But It has HitableObj tag.");// Hitable오브젝트
                }

                trmTmp = trmTmp.parent;
            }

            return healthCompo;
        }
        return null;
    }
}
