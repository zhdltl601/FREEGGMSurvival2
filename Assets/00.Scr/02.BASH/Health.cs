using UnityEditor;
using UnityEngine;

public abstract class Health : MonoBehaviour // ��ӹ޾Ƽ� ����� ��.
{
    [SerializeField]
    protected float _maxHp, _hp;
    bool _isDamageAble = true; //������������ ���� ���� ��.
    

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
    public virtual void Damage(float damage, float cooldown) //������������ ����� ���� �������.
    {
        Damage(damage);
        _isDamageAble = false;
        CancelInvoke(nameof(ToggleDamageAble));
        Invoke(nameof(ToggleDamageAble), cooldown);
    }


    void ToggleDamageAble() //����OFF
    {
        _isDamageAble = true;
    }
    public virtual void Damage(float damage, Vector3 dir, float power) //�˹��� ���� �������.
    {
        Damage(damage);
    }


    public abstract void Die();
}
