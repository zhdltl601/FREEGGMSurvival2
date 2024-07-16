using UnityEngine;

public abstract class StatusEffect 
{
    public float duration;
    public GameObject target;
    
    public StatusEffect(float _duration, GameObject _target)
    {
        duration = _duration;
        target = _target;
    }
    
    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}
