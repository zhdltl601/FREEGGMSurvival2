using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
public enum WeaponType
{
    //Bow,
    //Musket,
    Pistol,
    Revolver,
    Shotgun,
    Pig,
    Katana
}

[CreateAssetMenu(fileName = "SO/Weapon/WPData")]
public class WeaponSO : ScriptableObject
{
   public AnimatorOverrideController controller; //총의 애니메이션 컨트롤러. 총을 바꾸면, 이것도 바뀌는 방식으로 수행된다.
   public GameObject[] bullet;// BulletType과 같은 풀링의 키값배열을 사용하여 편하게 할 수도 있음. 배열로 한 이유는 기본사격과 대체사격(우클릭, 좌클릭)총알을 다르게 하기 위해서이다.
   public int maxAmmo;
}
