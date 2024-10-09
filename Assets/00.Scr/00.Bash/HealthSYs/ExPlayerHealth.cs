using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExPlayerHealth : ExHealth
{
    
    [SerializeField]
    AudioClip _hurtSOund,_deadSOund;
    [SerializeField] AudioSource _aud;

    [SerializeField] CinemachineImpulseSource _impulse;

    public float Armor=0;
    public override void GetDamage(float damage, Vector3 hitPoint, Vector3 normal)
    {
        damage /= 1+Armor;
        base.GetDamage(damage, hitPoint, normal);

       
    }
    public override void GetDamage(float damage)
    {
        if (_hurtSOund)
            _aud.PlayOneShot(_hurtSOund, 0.5f);

        if (_impulse)
            _impulse.GenerateImpulse();
        base.GetDamage(damage);
    }

    public override void Dead()
    {
        _aud.PlayOneShot(_deadSOund, 0.5f);
        //À¸¾Ç¹Î ³ª µÚÁü
        OnDeadEvent?.Invoke();
    }
}
