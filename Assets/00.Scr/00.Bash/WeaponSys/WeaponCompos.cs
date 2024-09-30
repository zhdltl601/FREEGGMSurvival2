using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCompo : MonoBehaviour
{
    public WeaponSO weaponSO;//무기 종류
    public int currentAmmo;//현재총알수
    public bool isHave;//소유여부

    [SerializeField]
    Transform _shotPos;
    [SerializeField]
    List<ParticleSystem> _particles;

    public WeaponCompo(WeaponSO weaponSO, int currentAmmo, bool isHave)//생성자밍
    {
        this.weaponSO = weaponSO;
        this.currentAmmo = currentAmmo;
        this.isHave = isHave;
    }

    public virtual void Fire(int bulletIndex)
    {
        if(UseAmmo())
        {
            Instantiate(weaponSO.bullet[bulletIndex],_shotPos.position,_shotPos.rotation);
        }
    }
        public void EffectInvoke(int EffectIndex)
    {
        _particles[EffectIndex].Play();
    }

    public bool UseAmmo()
    {
        if (currentAmmo - 1 >= 0)
            currentAmmo -= 1;
        return currentAmmo != 0;
    }
    public virtual void TestETSe()
    {

    }
}
