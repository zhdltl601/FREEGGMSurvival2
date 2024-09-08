using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAble
{
   public void GetDamage(float damage , Vector3 hitPoint , Vector3 normal);
   public void Hit(Vector3 hitPoint , Vector3 normal);
   public void Dead();


}
