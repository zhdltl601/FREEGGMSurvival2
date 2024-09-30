using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCompo : MonoBehaviour
{
    public WeaponSO weaponSO;//���� ����
    public int currentAmmo;//�����Ѿ˼�
    public bool isHave;//��������

    [SerializeField]
    Transform _shotPos;
    [SerializeField]
    List<ParticleSystem> _particles;

    public WeaponCompo(WeaponSO weaponSO, int currentAmmo, bool isHave)//�����ڹ�
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
