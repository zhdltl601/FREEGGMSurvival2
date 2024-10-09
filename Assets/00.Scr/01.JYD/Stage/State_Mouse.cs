using System;
using DG.Tweening;
using UnityEngine;

public class State_Mouse : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform player;
    
    private Vector3 _mousePos;
    private Camera mainCam;
    
    private bool isMousePress;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        isMousePress = Input.GetMouseButton(1);

        if (isMousePress)
        {
            _mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            target.position = _mousePos;
        }
        else
        {
            target.position = player.position;
        }
        
    }
}
