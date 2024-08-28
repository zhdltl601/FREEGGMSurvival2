using DG.Tweening;
using UnityEngine;

public class State_Mouse : MonoBehaviour
{
    [SerializeField] private float maxCamDistance;
    [SerializeField] private float minCamDistance;

    [SerializeField] private Transform target;
    
    private void FixedUpdate()
    {
        target.DOMove(GetDesiredCameraPosition(),1F);
    }
    
    private Vector3 GetDesiredCameraPosition()
    {
        Vector3 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = 0;
        Vector3 aimDirection = _mousePos - transform.position;
        float distance = Vector3.Distance(transform.position , _mousePos);
        distance = Mathf.Clamp(distance , minCamDistance , maxCamDistance);
                            
        Vector3 desiredPosition = transform.position + aimDirection.normalized * distance;
        
        return desiredPosition;      
    }
}
