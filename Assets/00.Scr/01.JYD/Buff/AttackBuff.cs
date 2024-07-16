using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuff : StatusEffect
{
    private int attackIncrease;
    
    public AttackBuff(float duration, GameObject target, int _attackIncrease) : base(duration, target)
    {
        attackIncrease = _attackIncrease;
    }

    public override void ApplyEffect()
    {
        Debug.Log("공격 버프 시작");
    }

    public override void RemoveEffect()
    {
        Debug.Log("공격 버프 끝남");
    }
}
