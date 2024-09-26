using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField]
    protected float _damage = 1, _criticalMulti = 2;
    protected virtual Health GetHealthCompo(Transform target)
    {
        if (target.transform.gameObject.CompareTag("Hitable") || target.transform.gameObject.CompareTag("Critical")) //태그가 Hitable이거나 Critical일 떄.
        {
            Transform trmTmp = target.transform.gameObject.transform;
            Health healthCompo;

            while (!trmTmp.TryGetComponent<Health>(out healthCompo))
            {
                if(trmTmp.parent == null)
                {
                    Debug.LogError("NotHitableObj. But It has HitableObj tag.");// Hitable오브젝트
                }

                trmTmp = trmTmp.parent;
            }

            return healthCompo;
        }
        return null;
    }
    protected virtual float DamageMultifiler(Transform target)
    {
        float damageMulti = 1;
        if (target.transform.gameObject.CompareTag("Critical"))
        {
            damageMulti = _criticalMulti;
        }

            return damageMulti;
    }

    protected virtual void ApplyDamage(Transform target)
    {
        Health healthCompo = GetHealthCompo(target);
        if (healthCompo)
        {
            float damageMulti = DamageMultifiler(target);

            healthCompo.Damage(_damage * damageMulti);
        }
    }
}
