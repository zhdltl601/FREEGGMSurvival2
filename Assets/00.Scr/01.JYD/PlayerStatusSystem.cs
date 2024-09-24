using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusSystem : MonoBehaviour
{

    private PlayerStatSystem _statSystem;
    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    private AttackBuff _attackBuff;
    
    private void Start()
    {
        _statSystem = GetComponent<PlayerStatSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ApplyBuff(new AttackBuff(3 , gameObject , 10));
            ApplyBuff(new BurnDebuff(3 , gameObject , 4 , 2.5f , this));
        }
    }

    public void ApplyBuff(StatusEffect effect)
    {
        effect.ApplyEffect();
        activeEffects.Add(effect);
        StartCoroutine(RemoveEffectAfterDuration(effect));
    }
    private IEnumerator RemoveEffectAfterDuration(StatusEffect effect)
    {
        yield return new WaitForSeconds(effect.duration);
        effect.RemoveEffect();
        activeEffects.Remove(effect);
    }
    public void RemoveAllEffects()
    {
        foreach (StatusEffect effect in activeEffects)
        {
            effect.RemoveEffect();
        }
        activeEffects.Clear();
    }
}
