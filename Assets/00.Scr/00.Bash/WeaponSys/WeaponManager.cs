using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int currentWeapon = 0; // 외부에서도 접근가능(임시로)

    public Animator animator;

    public List<WeaponCompo> weaponList = new List<WeaponCompo>();//무기리스트

    int _bFireHash, _tCreateHash, _bReloadHash, _fAmmoHash;//b:bool,t:Trigger,f:float;


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
        _tCreateHash = Animator.StringToHash("CreateObjectChange");//건설모드
        _bReloadHash = Animator.StringToHash("Reload");
        _fAmmoHash = Animator.StringToHash("AmmoPerMag");// (현재총알/탄창)  -> 잔탄수 표시 애니메이션 파라미터

        weaponList = GetComponentsInChildren<WeaponCompo>(true).ToList();
    }

    public void SwapWeaponNum(int idx1, int idx2)
    {
        BashUtils.SwapList(idx1, idx2, ref weaponList);
    }

    public void ChangeWeapon(int index)//무기 번호(숫자키 번호)입력
    {

        if (weaponList.Count >= index)
        {
            if (weaponList[index].isHave)
            {
                weaponList[currentWeapon].gameObject.SetActive(false);
                weaponList[index].gameObject.SetActive(true);

                currentWeapon = index;
                animator.runtimeAnimatorController = weaponList[index].weaponSO.controller;

                animator.SetFloat(_fAmmoHash, (float)weaponList[currentWeapon].currentAmmo / (float)weaponList[currentWeapon].weaponSO.maxAmmo);
                animator.SetTrigger("Reset");
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
        _audioSource.PlayOneShot(clip,0.5f);
    }
    void Update()
    {
        //나중에 인풋시스템 넣어야디

        animator.SetBool(_bFireHash, Input.GetKey(KeyCode.Mouse0));

        animator.SetBool(_bReloadHash, Input.GetKey(KeyCode.R));

        animator.SetBool("AltFire", Input.GetKey(KeyCode.Mouse1));
        //if (Input.GetKeyDown(KeyCode.Mouse1)) animator.SetTrigger(_tCreateHash);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeWeapon(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeWeapon(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeWeapon(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangeWeapon(8);
        }
    }
}
