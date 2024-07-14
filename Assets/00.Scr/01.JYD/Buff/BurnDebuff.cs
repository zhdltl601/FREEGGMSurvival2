using System.Collections;
using UnityEngine;

public class BurnDebuff : StatusEffect
{
    private float damagePerSecond;
    private float damageInterval;
    
    private MonoBehaviour coroutinePlayer;
    
    public BurnDebuff(float _duration, GameObject _target,float damagePerSecond, float damageInterval,MonoBehaviour coroutinePlayer) : base(_duration, _target)
    {
        this.damagePerSecond = damagePerSecond;
        this.damageInterval = damageInterval;
        this.coroutinePlayer = coroutinePlayer;
    }

    public override void ApplyEffect()
    {
        coroutinePlayer.StartCoroutine(InflictDamageOverTime());
        Debug.Log("불타기 시작함");
    }

    public override void RemoveEffect()
    {
        Debug.Log("불타기 끝남");
    }

    private IEnumerator InflictDamageOverTime()
    {
        while (duration > 0)
        {
            DealDamageToTarget(damagePerSecond);

            yield return new WaitForSeconds(damageInterval);

            duration -= Time.deltaTime;
        }
    }

    private void DealDamageToTarget(float damage)
    {
        //대미지 받는거 넣어야함.    
        Debug.Log("불불");
    }
}
