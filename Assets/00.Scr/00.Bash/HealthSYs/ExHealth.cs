using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExHealth : MonoBehaviour, IDamageAble, IAgentComponent // ��� �̷��� �ϸ� ��������SU�� �ǹ̰� ������ ��..
{
    public Action OnDeadEvent;
    public event Action OnHitEvent;
    
    
    [SerializeField]
    protected float _health,maxHealth;

    [SerializeField]
    protected ParticleSystem _hitImpact;
    
    void OnEnable()
    {
        _health = maxHealth;
    }

    public virtual void Initialize(Agent agent)
    {
       
    }

    public virtual void GetDamage(float damage, Vector3 hitPoint, Vector3 normal)
    {
        OnHitEvent?.Invoke();
        GetDamage(damage);
        Hit(hitPoint,normal);
    }
    public virtual void GetDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;
            Dead();
        }
    }

    public virtual void Hit(Vector3 hitPoint, Vector3 normal)
    {
        if (_hitImpact != null)
        {
            ParticleSystem effect = Instantiate(_hitImpact, transform.position, Quaternion.identity);
            effect.transform.forward = -normal;
        }
    }
    public virtual void Dead()
    {
        OnDeadEvent?.Invoke();
        Destroy(gameObject);
    }
}
