using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractInvenItemVis : MonoBehaviour
{
    [SerializeField] private SO_ItemBlueprint blueprint;
    private SO_Item soItem;
    private Camera uiCamera;
    private RectTransform rtrm;

    private void Start()
    {
        uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        soItem = GetComponent<InventoryItemVisual>().GetSO_Item;
        rtrm = ItemInfoPanel.Instance.GetComponent<RectTransform>();
    }

    public void OnPointerDown()
    {
        ItemInfoPanel.Instance.DisableInfoPanel();
        BlueprintViewer.Instance.SetCurrentItemBlueprint(blueprint);
    }

    public void OnPointerEnter()
    {
        ItemInfoPanel.Instance.SetItemInfoPanel(soItem.GetIcon,
            soItem.GetName, soItem.GetMaxAmount.ToString());
    }

    public static void OnPointerExit()
    {
        ItemInfoPanel.Instance.DisableInfoPanel();
    }
}
