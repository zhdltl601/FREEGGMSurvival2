using UnityEngine;

public class Bullet : RaycastDamager
{
    [SerializeField]
    ProjectileType _bulletType;
    [SerializeField]
    protected float _speed = 0.1f,_radius=0.2f; // 속도,총알 반지름
    [SerializeField]
    protected float _maxTime=40; //최대 LifeTime (생성 후 이 시간이 지나면 총알 사라짐)
    protected float _time;
    //[SerializeField]
    //LayerMask _layerMask;
    protected RaycastHit _hit;
    private void Start()
    {
        Init();
        
    }
    public virtual void Init()
    {
        _time = 0; // 총알 LifeTime 초기화
    }

    public virtual void FixedUpdate()
    {
        if(Scan(transform.position,transform.forward,_speed*Time.fixedDeltaTime))
        {
            HitEvent();
        }
        Move();
    }
    public virtual void HitEvent()
    {
        transform.position = _hit.point;
        Destroy(gameObject);
    }
    protected virtual void Move()
    {
        transform.position += transform.forward * _speed*Time.fixedDeltaTime; //총알 이동(레이로 쏜 궤적에 없으면 이동)
        _time += Time.deltaTime;

        if (_time >= _maxTime)
        {
            Destroy(gameObject);
            //Pool.Instance.Get(_bulletType, gameObject); // 총알 풀링하기
        }
    }

    //protected virtual void Scan()
    //{
    //    if (Physics.SphereCast(transform.position, _radius, transform.forward, out _hit, _speed*Time.deltaTime, _layerMask, QueryTriggerInteraction.Collide))
    //    {
    //        ApplyDamage(_hit.transform);//뎀지 적용밍

    //        Pool.Instance.Get(_bulletType, gameObject); // 총알 풀링 해주기 ㅎㅎ
    //    }
    //}
}
