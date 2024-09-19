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
   public AnimatorController controller;
   public GameObject[] bullet;
   public int maxAmmo;
}
