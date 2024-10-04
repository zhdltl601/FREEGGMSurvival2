using System;
using UnityEditor;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [SerializeField] private FishSystem FishSystem;
    [SerializeField] private LayerMask whatIsWater;

    [SerializeField] private Transform aim;
    [SerializeField] private bool aimOnWater;
    
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;;
    }

    private void Update()
    {
        aimOnWater = CheckAimOnWater();
        
        if (Input.GetMouseButtonDown(0) && aimOnWater)
        {
            OnFishing();
        }
    }
    private bool CheckAimOnWater()
    {
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, whatIsWater))
        {
            return true; 
        }

        return false;
    }
    
    //≥¨Ω√ Ω√¿€
    private void OnFishing()
    {
        FishSystem.gameObject.SetActive(true);
    }

    private void OffFishing()
    {
        FishSystem.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(mainCam.transform.position , mainCam.transform.forward * 25);
    }
}
