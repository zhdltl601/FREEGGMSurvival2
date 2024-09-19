using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int currentWeapon = 0; // 외부에서도 접근가능(임시로)
    
    public Animator animator;
    
    public List<WeaponCompo> weaponList = new List<WeaponCompo>();//무기리스트

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
        _fAmmoHash = Animator.StringToHash("AmmoPerMag");// (현재총알/탄창)  -> 잔탄수 표시 애니메이션 파라미터
    }

    public void ChangeWeapon(int index)//무기 번호(숫자키 번호)입력
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
        //나중에 인풋시스템 넣어야디

        animator.SetBool(_bFireHash, Input.GetKey(KeyCode.Mouse0));

        animator.SetBool(_bReloadHash, Input.GetKey(KeyCode.R));
    }

    
}
