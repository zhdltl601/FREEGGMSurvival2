using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    protected float damage;
    protected virtual ExHealth GetHealthCompo(Transform target, ref float damageMulti)
    {
        if (target.transform.gameObject.CompareTag("Hitable") || target.transform.gameObject.CompareTag("Critical"))//크리티컬이나 히트애이블이 달린 오브젝트가 타깃.
        {
            Transform trmTmp = target.transform.gameObject.transform;
            ExHealth healthCompo;

            while (!trmTmp.TryGetComponent<ExHealth>(out healthCompo))
            {
                if (trmTmp.parent == null)
                {
                    Debug.LogError("NotHitableObj. But It has HitableObj tag.");
                    return null;
                }

                trmTmp = trmTmp.parent;
            }
            if (target.transform.gameObject.CompareTag("Critcal"))
            {
                damageMulti = 2;
            }
            else
            {
                damageMulti = 1;
            }
            return healthCompo;
        }
        return null;
    }
}
