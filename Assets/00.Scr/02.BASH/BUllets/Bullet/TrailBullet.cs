using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBullet : Bullet
{
    [SerializeField]
    TrailRenderer _trailRenderer; //풀링할 때 총알의 트레일이 기괴하게 되는 것을 방지하고자 넣어놓았음.
    public override void Init()
    {
        _trailRenderer.Clear();
        base.Init();
    }
}
