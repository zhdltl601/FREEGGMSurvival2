using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _speed;
    void FixedUpdate()
    {
        _target.rotation = Quaternion.Lerp(_target.rotation,transform.rotation, Time.fixedDeltaTime*_speed);
    }
}
