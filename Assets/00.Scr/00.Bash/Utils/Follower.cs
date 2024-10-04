using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    Transform _target;
    void Update()
    {
        _target.SetPositionAndRotation(transform.position, transform.rotation);
    }


    //#if UNITY_EDITOR

    //    {
    //        _target.SetPositionAndRotation(transform.position, transform.rotation);
    //    }
    //#endif
}
