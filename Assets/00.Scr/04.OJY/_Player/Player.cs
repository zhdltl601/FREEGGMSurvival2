using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Inv Visual/Item Selecting")]
    public LayerMask _lm_item;
    private Camera _cam;

    [SerializeField] private Inventory _inventory;
    [SerializeField] private Crafter _crafter;

    [Header("Debugging")]
    [SerializeField] private SO_Item itemTOADD;

    private void Awake()
    {
        _cam = Camera.main;
        _inventory = GetComponentInChildren<Inventory>();
    }
    
    private void Start()
    {
        InventoryUI.Instance.ToggleInventory();
        InventoryUI.Instance.SetCrafter(_crafter);
        InventoryUI.Instance.SetInv(_inventory);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryUI.Instance.ToggleInventory();
        }
        //if (Input.GetKeyDown(KeyCode.B)) Inventory.AddBluePrint(bpToAdd);
        if (Input.GetKeyDown(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift)) _inventory.TryAddItemToInventory(itemTOADD, 1);
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)) _inventory.TrySubtractItemToInventory(itemTOADD, 1);
        if (Input.GetKeyDown(KeyCode.P)) _inventory.Debug_PrintShit();
    }

    private void CancelCraft()
    {

    }
}
