using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    private Camera uiCam;
    [SerializeField] private LayerMask _lm_item;
    private bool isMouseOnItem = false;

    private void Awake()
    {
        uiCam = GetComponent<Camera>();
    }

    void Update()
    {
        PointerOnItemVis();
        if(Input.GetMouseButtonDown(0))
        {
            PointerDownOnItemVis();
        }
    }

    private void PointerOnItemVis()
    {
        Ray mDir = uiCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(uiCam.transform.position, mDir.direction,
            out RaycastHit hitInfo, Mathf.Infinity, _lm_item))
        {
            hitInfo.transform.GetComponent<InteractInvenItemVis>().OnPointerEnter();
            isMouseOnItem = true;
        }
        else
        {
            if(isMouseOnItem == true)
            {
                InteractInvenItemVis.OnPointerExit();
                isMouseOnItem = false;
            }
        }
    }

    private void PointerDownOnItemVis()
    {
        Ray mDir = uiCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(uiCam.transform.position, mDir.direction,
            out RaycastHit hitInfo, Mathf.Infinity, _lm_item))
        {
            hitInfo.transform.GetComponent<InteractInvenItemVis>().OnPointerDown();
        }
    }
}
