using UnityEngine;

public class Bullet : RaycastDamager
{
    [SerializeField]
    ProjectileType _bulletType;
    [SerializeField]
    protected float _speed = 0.1f,_radius=0.2f; // �ӵ�,�Ѿ� ������
    [SerializeField]
    protected float _maxTime; //�ִ� LifeTime (���� �� �� �ð��� ������ �Ѿ� �����)
    protected float _time;
    [SerializeField]
    LayerMask _layerMask;
    protected RaycastHit _hit;
    private void Start()
    {
        Init();
        
    }
    public virtual void Init()
    {
        _time = 0; // �Ѿ� LifeTime �ʱ�ȭ
    }

    public virtual void FixedUpdate()
    {
        if(Scan(transform.position,transform.forward,_radius,_speed*Time.fixedDeltaTime))
            Destroy(gameObject);
        Move();
    }
    protected virtual void Move()
    {
        transform.position += transform.forward * _speed*Time.fixedDeltaTime; //�Ѿ� �̵�(���̷� �� ������ ������ �̵�)
        _time += Time.deltaTime;

        if (_time >= _maxTime)
        {
            Destroy(gameObject);
            //Pool.Instance.Get(_bulletType, gameObject); // �Ѿ� Ǯ���ϱ�
        }
    }

    //protected virtual void Scan()
    //{
    //    if (Physics.SphereCast(transform.position, _radius, transform.forward, out _hit, _speed*Time.deltaTime, _layerMask, QueryTriggerInteraction.Collide))
    //    {
    //        ApplyDamage(_hit.transform);//���� �����

    //        Pool.Instance.Get(_bulletType, gameObject); // �Ѿ� Ǯ�� ���ֱ� ����
    //    }
    //}
}
