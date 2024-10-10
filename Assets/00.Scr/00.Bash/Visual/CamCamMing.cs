using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCamMing : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        _animator.SetTrigger("Trigger");
    }
}
