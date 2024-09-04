using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoSingleton<DebugUI>
{
    [Header("Debug")]
    public List<Text> list;

    public SO_ItemBlueprint bpToAdd;
    public SO_Item itemToAdd;

    public Crafter crafter;
    public Inventory inv;
    public int fuck;

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
    /// <param name="amount">amount is how much item has changed</param>
    //todo : remove hard reference
    private void HandleOnItemChanged(SO_Item changedItem, int amount)
    {
        int visMax = changedItem.GetVisPosInv.Count;
        int currentItemAmount = inv.GetInventory[changedItem];
        int beforeItemAmount = currentItemAmount - amount;

        bool isIncreassing = amount > 0;
        if (isIncreassing)
        {
            bool isVisOver = visMax < currentItemAmount;
            if (!isVisOver)
            {
                InventoryItemVisual.UpdateItemVisAdd(changedItem, amount);
            }
            else if (visMax > beforeItemAmount)
            {
                //calc if legit
                InventoryItemVisual.UpdateItemVisSet(changedItem, visMax);
            }
        }
        else
        {
            bool canDecreease = visMax > currentItemAmount;
            if (canDecreease)
            {
                InventoryItemVisual.UpdateItemVisSet(changedItem, currentItemAmount);
            }
        }
        void DebugText()
        {
            list[0].text = inv.GetInventory[changedItem].ToString();//; changedItem
            //list[1].text = "itemBeforeCalc" + itemAmountBeforeCalc;
            //list[2].text = changeValue.ToString();
        }
        DebugText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) Inventory.AddBluePrint(bpToAdd);
        if (Input.GetKeyDown(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift)) inv.TryAddItemToInventory(itemToAdd, fuck);
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)) inv.TrySubtractItemToInventory(itemToAdd, fuck);

        if (Input.GetKeyDown(KeyCode.C)) inv.CancelCraft(crafter);

        if (Input.GetKeyDown(KeyCode.P)) inv.Debug_PrintShit();

        void ObjSelect()
        {
            Ray mDir = _cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_cam.transform.position, mDir.direction * 50, Color.red, 2);
            if(Physics.Raycast(_cam.transform.position, mDir.direction, out RaycastHit hitInfo, 50, _lm_item))
            {
                if (hitInfo.transform.TryGetComponent(out InventoryItemVisual c))
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
}
