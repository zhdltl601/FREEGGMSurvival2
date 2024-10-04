using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum ResourceType
{
    Wood,
    Stone
}
public class ExResourceHealth : ExHealth
{
    [SerializeField]
    protected ResourceType _resourceType;

    public override void GetDamage(float damage, Vector3 hitPoint, Vector3 normal)
    {
        //전용도구가 아니면 노데미지밍..
        //if()
        base.GetDamage(damage/999, hitPoint, normal);
        transform.DOPunchScale(Vector3.one/15, 0.1f);
    }

    public void GetResourceDamge(ResourceType res,float damage, Vector3 hitPoint, Vector3 normal)
    {

        if(res == _resourceType)
        {
        base.GetDamage(damage*2, hitPoint, normal);
        }
        transform.DOPunchScale(Vector3.one/5, 0.2f);
    }
}
