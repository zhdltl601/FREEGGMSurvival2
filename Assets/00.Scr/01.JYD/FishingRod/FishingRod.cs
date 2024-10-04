using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [SerializeField] private FishSystem FishSystem;
    [SerializeField] private LayerMask whatIsWater;

    [SerializeField] private Transform aim;
    
    private Camera mainCam;
        
    private void Awake()
    {
        mainCam = Camera.main;;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CheckAimOnWater())
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
        PlayerInput.Instance.canRotate = false;
        FishSystem.gameObject.SetActive(true);
    }

    private void OffFishing()
    {
        FishSystem.FishEnd();
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawRay(mainCam.transform.position , mainCam.transform.forward * 25);
    }
}
