using DG.Tweening;
using UnityEngine;

public class State_Mouse : MonoBehaviour
{
    [SerializeField] private float maxCamDistance;
    [SerializeField] private float minCamDistance;

    [SerializeField] private Transform target;

    private Vector3 _mousePos;
    private void FixedUpdate()
    {
        if(IsMouseMoved())
            target.DOMove(GetDesiredCameraPosition(),1F);
    }
    
    private bool IsMouseMoved()
    {
        return Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;
    }
    
    private Vector3 GetDesiredCameraPosition()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = 0;
        Vector3 aimDirection = _mousePos - transform.position;
        float distance = Vector3.Distance(transform.position , _mousePos);
        distance = Mathf.Clamp(distance , minCamDistance , maxCamDistance);
                            
        Vector3 desiredPosition = transform.position + aimDirection.normalized * distance;
        
        return desiredPosition;      
    }
}
