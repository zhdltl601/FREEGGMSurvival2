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
   public AnimatorOverrideController controller; //���� �ִϸ��̼� ��Ʈ�ѷ�. ���� �ٲٸ�, �̰͵� �ٲ�� ������� ����ȴ�.
   public GameObject[] bullet;// BulletType�� ���� Ǯ���� Ű���迭�� ����Ͽ� ���ϰ� �� ���� ����. �迭�� �� ������ �⺻��ݰ� ��ü���(��Ŭ��, ��Ŭ��)�Ѿ��� �ٸ��� �ϱ� ���ؼ��̴�.
   public int maxAmmo;
}
