using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int currentWeapon = 0; // �ܺο����� ���ٰ���(�ӽ÷�)
    
    public Animator animator;
    
    public List<WeaponCompo> weaponList = new List<WeaponCompo>();//���⸮��Ʈ

    int _bFireHash, _bReloadHash,_fAmmoHash;//b:bool,t:Trigger,f:float;

    [SerializeField]
    AudioSource _audioSource;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        animator = GetComponent<Animator>();

        _bFireHash = Animator.StringToHash("Fire");
        _bReloadHash = Animator.StringToHash("Reload");
        _fAmmoHash = Animator.StringToHash("AmmoPerMag");// (�����Ѿ�/źâ)  -> ��ź�� ǥ�� �ִϸ��̼� �Ķ����
    }

    public void ChangeWeapon(int index)//���� ��ȣ(����Ű ��ȣ)�Է�
    {
        if(weaponList.Count <= index)
        {
            if (weaponList[index].isHave)
            {
                currentWeapon = index;
                animator.runtimeAnimatorController = weaponList[index].weaponSO.controller;

                animator.SetFloat(_fAmmoHash, (float)weaponList[currentWeapon].currentAmmo / (float)weaponList[currentWeapon].weaponSO.maxAmmo);
            }
        }

    }

    public void TryUseWeapon(int fireType)
    {
        weaponList[currentWeapon].Fire(fireType);
        animator.SetFloat(_fAmmoHash, (float)weaponList[currentWeapon].currentAmmo / (float)weaponList[currentWeapon].weaponSO.maxAmmo);
    }

    public void PlayEffect(int effectType)
    {
        weaponList[currentWeapon].EffectInvoke(effectType);
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
    void Update()
    {
        //���߿� ��ǲ�ý��� �־�ߵ�

        animator.SetBool(_bFireHash, Input.GetKey(KeyCode.Mouse0));

        animator.SetBool(_bReloadHash, Input.GetKey(KeyCode.R));
    }

    
}
