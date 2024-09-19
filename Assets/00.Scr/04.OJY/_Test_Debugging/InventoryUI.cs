using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoSingleton<InventoryUI>
{
    [Header("Debug")]
    public List<Text> dbg_list;

    [Header("Inv Visual/def")]
    public Transform _piv;

    [Header("Inv Visual/Item Selecting")]
    public LayerMask _lm_item;

    private Camera _camInv;

    private Crafter _currentCrafter;
    private Inventory _playerInventory;//static?
    
    protected override void Awake()
    {
        base.Awake();
_camInv = Camera.main;//
        Inventory.OnItemChanged += HandleOnItemChanged;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        Inventory.OnItemChanged -= HandleOnItemChanged;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) _playerInventory.CancelCraft(_currentCrafter);

        void ObjSelect()
        {
            Ray mDir = _camInv.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_camInv.transform.position, mDir.direction * 50, Color.red, 2);
            if (Physics.Raycast(_camInv.transform.position, mDir.direction, out RaycastHit hitInfo, 50, _lm_item))
            {
                if (hitInfo.transform.TryGetComponent(out InventoryItemVisual c)) _playerInventory.TryAddItemToCraft(c.GetSO_Item, _currentCrafter);
                else Debug.LogError("ItemDoesntHave InventoryItem Comp" + hitInfo.transform.name);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) ObjSelect();
    }
    public void SetCrafter(Crafter crafter)
    {
        _currentCrafter = crafter;
    }
    public void SetInv(Inventory inv)
    {
        _playerInventory = inv;
    }
    /// <param name="amount">amount is how much item has changed</param>
    private void HandleOnItemChanged(SO_Item changedItem, int amount)
    {
        int visMax = changedItem.GetVisPosInv.Count;
        int currentItemAmount = _playerInventory.GetInventory[changedItem];
        int beforeItemAmount = currentItemAmount - amount;

        bool isIncreassing = amount > 0;
        if (isIncreassing)
        {
            bool isVisOver = visMax < currentItemAmount;
            if (!isVisOver) InventoryItemVisual.UpdateItemVisAdd(changedItem, amount);
            else if (visMax > beforeItemAmount) InventoryItemVisual.UpdateItemVisSet(changedItem, visMax);
        }
        else
        {
            bool canDecreease = visMax > currentItemAmount;
            if (canDecreease) InventoryItemVisual.UpdateItemVisSet(changedItem, currentItemAmount);
        }
        void DebugText()
        {
            //dbg_list[0].text = _playerInventory.GetInventory[changedItem].ToString();//; changedItem
        }
        DebugText();
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
        bool isInactive = !gameObject.activeSelf;
        if (isInactive) InvON();
        else InvOFF();
        gameObject.SetActive(isInactive);
    }
}
