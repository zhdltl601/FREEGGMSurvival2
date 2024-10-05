using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int currentWeapon = 0; // �ܺο����� ���ٰ���(�ӽ÷�)

    public Animator animator;

    public List<WeaponCompo> weaponList = new List<WeaponCompo>();//���⸮��Ʈ

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
        _tCreateHash = Animator.StringToHash("CreateObjectChange");//�Ǽ����
        _bReloadHash = Animator.StringToHash("Reload");
        _fAmmoHash = Animator.StringToHash("AmmoPerMag");// (�����Ѿ�/źâ)  -> ��ź�� ǥ�� �ִϸ��̼� �Ķ����

        weaponList = GetComponentsInChildren<WeaponCompo>(true).ToList();
    }

    public void SwapWeaponNum(int idx1, int idx2)
    {
        BashUtils.SwapList(idx1, idx2, ref weaponList);
    }

    public void ChangeWeapon(int index)//���� ��ȣ(����Ű ��ȣ)�Է�
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
        //���߿� ��ǲ�ý��� �־�ߵ�

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
