using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private float maxMouseMovementX;
    [SerializeField] private float minMouseMovementX;
    [SerializeField] private float maxMouseMovementY;
    [SerializeField] private float minMouseMovementY;

    private float clampMouseX;
    private float clampMouseY;

    [SerializeField] private float mouseSensitivity;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        TiltScreen();
    }

    private void TiltScreen()
    {
        float mouseMovementX = Input.GetAxis("Mouse X");
        float mouseMovementY = Input.GetAxis("Mouse Y");

        clampMouseX += mouseMovementX * Time.deltaTime *
            mouseSensitivity;

        clampMouseX = Mathf.Clamp(clampMouseX,
            minMouseMovementX, maxMouseMovementX);

        clampMouseY -= mouseMovementY * Time.deltaTime * 
            mouseSensitivity;

        clampMouseY = Mathf.Clamp(clampMouseY,
            minMouseMovementY, maxMouseMovementY);

        cam.transform.localRotation = Quaternion.Euler(
            clampMouseY, clampMouseX, 0);
    }
}
