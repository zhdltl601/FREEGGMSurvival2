using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotRotMing : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private float looksensitivity;
    [SerializeField] private float cameraRotationLimit;

    [Header("ReadOnly")]
    [SerializeField] private float currentCameraRotationX;

    [Header("GetComponent")]
    private Rigidbody playerRigid;
    [SerializeField] private Transform virtualCamera;

    private void Awake()
    {
        playerRigid = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float yRotation = Input.GetAxisRaw("Mouse X");

        CameraRotation(xRotation);
        CharacterRotation(yRotation);
    }
    private void CameraRotation(float xRotation)
    {
        // 상하 카메라 회전
        float cameraRotationX = xRotation * looksensitivity;
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        virtualCamera.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
    private void CharacterRotation(float yRotation)
    {
        // 좌우 카메라 회전
        Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * looksensitivity;
        playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(characterRotationY));
    }
}
