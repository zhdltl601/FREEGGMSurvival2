using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _camRot;
    public Transform GetCameraRot => _camRot;
    public void SetCameraRotation(Quaternion value)
    {
        _camRot.transform.rotation = value;
    }
}