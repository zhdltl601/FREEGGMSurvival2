using UnityEditor;
using UnityEngine;

public abstract class Health : MonoBehaviour // 상속받아서 사용할 것.
{
    [SerializeField]
    protected float _maxHp, _hp;
    bool _isDamageAble = true; //무적프레임을 위해 만든 것.
    

    public virtual void Awake()
    {
        _hp = _maxHp;
    }
    public virtual void Damage(float damage)
    {
        if (_isDamageAble)
        {
            _hp -= damage;
            _hp = Mathf.Clamp(_hp, 0, _maxHp);
            if (_hp <= 0)
            {
                Die();
            }
        }
    }
    public virtual void Damage(float damage, float cooldown) //무젝프레임을 만들기 위해 만들어짐.
    {
        Damage(damage);
        _isDamageAble = false;
        CancelInvoke(nameof(ToggleDamageAble));
        Invoke(nameof(ToggleDamageAble), cooldown);
    }


    void ToggleDamageAble() //무적OFF
    {
        _isDamageAble = true;
    }
    public virtual void Damage(float damage, Vector3 dir, float power) //넉백을 위해 만들어짐.
    {
        Damage(damage);
    }


    public abstract void Die();
}
