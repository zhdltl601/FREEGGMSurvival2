using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField]
    protected float _damage = 1, _criticalMulti = 2;
    protected virtual ExHealth GetHealthCompo(Transform target)
    {
        if (target.transform.gameObject.CompareTag("Hitable") || target.transform.gameObject.CompareTag("Critical")) //�±װ� Hitable�̰ų� Critical�� ��.
        {
            Transform trmTmp = target.transform.gameObject.transform;
            ExHealth healthCompo;

            while (!trmTmp.TryGetComponent<ExHealth>(out healthCompo))
            {
                if(trmTmp.parent == null)
                {
                    Debug.LogError("NotHitableObj. But It has HitableObj tag.");// Hitable������Ʈ
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
        ExHealth healthCompo = GetHealthCompo(target);
        if (healthCompo)
        {
            float damageMulti = DamageMultifiler(target);

            healthCompo.GetDamage(_damage * damageMulti);
        }
    }
    protected virtual void ApplyDamage(Transform target, Vector3 hitPoint, Vector3 normal)
    {
        ExHealth healthCompo = GetHealthCompo(target);
        if (healthCompo)
        {
            float damageMulti = DamageMultifiler(target);

            healthCompo.GetDamage(_damage * damageMulti,hitPoint,normal);
        }
    }
}
