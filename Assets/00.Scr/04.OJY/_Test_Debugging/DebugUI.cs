using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoSingleton<DebugUI>
{
    [Header("Debug")]
    public List<Text> list;
    public SO_ItemBlueprint bp;
    public SO_Item item;

    public Crafter crafter;
    public Inventory inv;

    [Header("Inv Visual/def")]
    public Transform _piv;
    

    [Header("Inv Visual/Item Selecting")]
    public LayerMask _lm_item;
    private Camera _cam;

    protected override void Awake()
    {
        base.Awake();
        UpdateUI();
        _cam = Camera.main;
        Inventory.OnItemChanged += HandleOnItemChanged;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        Inventory.OnItemChanged -= HandleOnItemChanged;
    }
    private void HandleOnItemChanged(SO_Item changedItem, int amount)
    {
        int itemAmount = inv.GetInventory[changedItem];
        void DebugText()
        {
            list[0].text = changedItem + " " + itemAmount;
        }
        DebugText();

        int itemAmountBeforeCalc = itemAmount - amount;
        int posSpaceLeft = changedItem.GetPosMaxCount - itemAmountBeforeCalc;

        int changeValue;
        bool isIncreassing = amount > 0;
        //if (isIncreassing)
        //{
        //    changeValue = posSpaceLeft > 0 ? posSpaceLeft - amount : 0;
        //
        //    if (itemAmountBeforeCalc <= changedItem.GetPosMaxCount)
        //        InventoryItem.UpdateItemVisual(changedItem, changeValue);
        //}
        //else
        //{
        //    if (itemAmountBeforeCalc < changedItem.GetPosMaxCount)
        //        InventoryItem.UpdateItemVisual(changedItem, changeValue);
        //}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) Inventory.AddBluePrint(bp);
        if (Input.GetKeyDown(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift)) inv.TryAddItemToInventory(item, 2);
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)) print(inv.TrySubtractItemToInventory(item));
        if (Input.GetKeyDown(KeyCode.K)) inv.TryAddItemToCraft(item, crafter);
        if (Input.GetKeyDown(KeyCode.C)) inv.CancelCraft(crafter);

        if (Input.GetKeyDown(KeyCode.P)) inv.Debug_PrintShit();

        void ObjSelect()
        {
            Ray mDir = _cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_cam.transform.position, mDir.direction * 50, Color.red, 2);
            if(Physics.Raycast(_cam.transform.position, mDir.direction, out RaycastHit hitInfo, 50, _lm_item))
            {
                if (hitInfo.transform.TryGetComponent(out InventoryItem c))
                {
                    inv.TryAddItemToCraft(c.GetSO_Item, crafter);
                    print(c.name);
                }
                else
                {
                    Debug.LogError("ItemDoesntHave InventoryItem Comp" + hitInfo.transform.name);
                }

            }
        }
        //if (Input.GetKeyDown(KeyCode.Mouse0)) ObjSelect();

    }
    public void ToggleInventory()
    {
        void InvON()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        void InvOFF()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        bool result = !gameObject.activeSelf;
        if (result) InvON();
        else InvOFF();
        gameObject.SetActive(result);
    }
    private void UpdateUI()
    {
        void UpdateInventory()
        {

        }
        void UpdateTable()
        {

        }
        UpdateInventory();
        UpdateTable();
    }
    public void OnBtnAdd(SO_Item itemToAdd)
    {

    }
    public void OnBtnDrop(SO_Item itemToDrop)
    {

    }
    public void OnBtnTryCraft()
    {
        inv.TryCraftItem();
    }
    public void OnBtnFuck()
    {

    }
}
