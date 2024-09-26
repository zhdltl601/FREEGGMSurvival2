using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBullet : Bullet
{
    [SerializeField]
    TrailRenderer _trailRenderer; //Ǯ���� �� �Ѿ��� Ʈ������ �Ⱬ�ϰ� �Ǵ� ���� �����ϰ��� �־������.
    public override void Init()
    {
        _trailRenderer.Clear();
        base.Init();
    }
}
