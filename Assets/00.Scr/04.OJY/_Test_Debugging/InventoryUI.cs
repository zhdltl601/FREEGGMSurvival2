using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoSingleton<InventoryUI>
{
    [Header("Debug")]
    public List<Text> dbg_list;
    [SerializeField] private SO_Item itemTOADD;
    [SerializeField] private SO_ItemBlueprint bpToAdd;

    [Header("Inv Visual/def")]
    public Transform _piv;

    [Header("Inv Visual/Item Selecting")]
    public LayerMask _lm_item;

    [SerializeField] private Camera _camInv;
    public Crafter CurrentCrafter { get; set; }
    public Inventory PlayerInventory { get; set; }
    protected override void Awake()
    {
        base.Awake();
//_camInv = Camera.main;//
        Inventory.OnItemChanged += HandleOnItemChanged;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        Inventory.OnItemChanged -= HandleOnItemChanged;
    }
    private void Update()
    {
        void KeyInput()
        {
            if (Input.GetKeyDown(KeyCode.C)) PlayerInventory.CancelCraft(CurrentCrafter);
        }
        void DebugInput()
        {
            if (Input.GetKeyDown(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift)) PlayerInventory.TryAddItemToInventory(itemTOADD);
            if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)) PlayerInventory.TrySubtractFromInventory(itemTOADD);
            if (Input.GetKeyDown(KeyCode.B)) Inventory.AddBluePrint(bpToAdd);
        }
        void ObjSelect()
        {
            Ray mDir = _camInv.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_camInv.transform.position, mDir.direction * 50, Color.red, 2);
            if (Physics.Raycast(_camInv.transform.position, mDir.direction, out RaycastHit hitInfo, 50, _lm_item))
            {
                if (hitInfo.transform.TryGetComponent(out InventoryItemVisual c)) PlayerInventory.TryAddItemToCraft(c.GetSO_Item, CurrentCrafter);
                else Debug.LogError("ItemDoesntHave InventoryItem Comp" + hitInfo.transform.name);
            }
        }
        KeyInput();
        DebugInput();
        if (Input.GetKeyDown(KeyCode.Mouse0)) ObjSelect();
    }

    /// <param name="amount">amount is how much item has changed</param>
    private void HandleOnItemChanged(SO_Item changedItem, int amount)
    {
        int visMax = changedItem.GetVisPosInv.Count;
        int currentItemAmount = PlayerInventory.GetInventory[changedItem];
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
    }
    public void SetActive(bool active)
    {
        if (active) ON();
        else OFF();
        gameObject.SetActive(active);
        void ON()
        {
            _camInv.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        void OFF()
        {
            PlayerInventory.CancelCraft(CurrentCrafter);
            _camInv.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public bool ToggleInventory()
    {
        bool isInactive = !gameObject.activeSelf;
        SetActive(isInactive);
        return isInactive;
    }
}
