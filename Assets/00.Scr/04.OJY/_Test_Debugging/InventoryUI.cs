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
    public Inventory PlayerInventory { get; set; }
    protected override void Awake()
    {
        base.Awake();
        //_camInv = Camera.main;//
        SetActive(false);
        Inventory.OnItemChanged += HandleOnItemChanged;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        Inventory.OnItemChanged -= HandleOnItemChanged;
    }
    private void Start()
    {
        UIManager.Instance.ToggleQuest();
    }
    private void Update()
    {
        void KeyInput()
        {
            if (Input.GetKeyDown(KeyCode.C)) PlayerInventory.CancelCraft(Player.CurrentCrafter);
        }
        void DebugInput()
        {
            if (Input.GetKeyDown(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift)) PlayerInventory.TryAddItemToInventory(itemTOADD);
            if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)) PlayerInventory.TrySubtractFromInventory(itemTOADD);
            if (Input.GetKeyDown(KeyCode.Z)) Inventory.AddBluePrint(bpToAdd);
            if (Input.GetKeyDown(KeyCode.Backspace)) PlayerInventory.CancelCraft(Player.CurrentCrafter);
        }
        void ObjSelect()
        {
            Ray mDir = _camInv.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_camInv.transform.position, mDir.direction * 50, Color.red, 2);
            if (Physics.Raycast(_camInv.transform.position, mDir.direction, out RaycastHit hitInfo, 50, _lm_item))
            {
                if (hitInfo.transform.TryGetComponent(out InventoryItemVisual c))
                {
                    c.SelectObj();
                    PlayerInventory.TryAddItemToCraft(c.GetSO_Item, Player.CurrentCrafter);
                    //c.DestroyThisObj();
                }
                else Debug.LogError("ItemDoesntHave InventoryItem Comp" + hitInfo.transform.name);
            }
        }
        void ObjEquip()
        {
            Ray mDir = _camInv.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_camInv.transform.position, mDir.direction * 50, Color.red, 2);
            if (Physics.Raycast(_camInv.transform.position, mDir.direction, out RaycastHit hitInfo, 50, _lm_item))
            {
                //PlayerInventory.GetWeaponManager.ChangeWeapon()
            }
        }
        void ObjFocus()
        {
            Ray mDir = _camInv.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_camInv.transform.position, mDir.direction, out RaycastHit hitInfo, 50, _lm_item))
            {
                if (hitInfo.transform.TryGetComponent(out InventoryItemVisual c))
                {
                    //c.SetItemInfoPanelOn(c.GetSO_Item, PlayerInventory.GetInventory[c.GetSO_Item]);//, hitInfo.point, _camInv);
                    Vector2 canvasPoint = RectTransformUtility.WorldToScreenPoint(_camInv, hitInfo.point);
                    ItemInfoPanel.Instance.SetItemInfoPanel(
                        c.GetSO_Item.GetIcon, c.GetSO_Item.GetName,
                        PlayerInventory.GetInventory[c.GetSO_Item],
                        canvasPoint, _camInv);
                }
            }
            else ItemInfoPanel.Instance.DisableInfoPanel();

        }
        if (Input.GetKeyDown(KeyCode.J))
        {

            dbg_list[0].text = "test : " + Player.CurrentCrafter.GetItemsOnTable[itemTOADD];
            dbg_list[1].text = "test2 : " + PlayerInventory.GetInventory[itemTOADD];
        }
        ObjFocus();
        KeyInput();
        DebugInput();
        if (Input.GetKeyDown(KeyCode.Mouse0)) ObjSelect();
        if (Input.GetKeyDown(KeyCode.Mouse1)) ObjEquip();
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
            if (BshAmiKlr.GameManager.Canf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None; 
            }
        }
        void OFF()
        {
            _camInv.gameObject.SetActive(false);
            if (BshAmiKlr.GameManager.Canf)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayerInventory.CancelCraft(Player.CurrentCrafter);
            }
        }
    }
    public bool ToggleInventory()
    {
        bool isInactive = !gameObject.activeSelf;
        BlueprintViewer.Instance.RemoveAllByCrafting();
        PlayerInventory.CancelCraft(Player.CurrentCrafter);
        SetActive(isInactive);
        return isInactive;
    }
}
