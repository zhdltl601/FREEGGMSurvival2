using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUI : MonoSingleton<DebugUI>
{
    [Header("Debug")]
    public SO_ItemBlueprint bp;
    public SO_Item item;

    public Crafter crafter;
    public Inventory inv;
    protected override void Awake()
    {
        base.Awake();
        UpdateUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) Inventory.AddBluePrint(bp);
        if (Input.GetKeyDown(KeyCode.P)) inv.TryAddItemToInventory(item);
        if (Input.GetKeyDown(KeyCode.L)) inv.TryAddItemToCraft(item, crafter);
        if (Input.GetKeyDown(KeyCode.K)) inv.RemoveAllItemToCraft(crafter);
        if (Input.GetKeyDown(KeyCode.C)) inv.PrintShit();
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
    public void OnBtnFuck()
    {

    }
}
