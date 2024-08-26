using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Crafter : MonoBehaviour
{
    private readonly Dictionary<SO_Item, int> _itemsOnTable = new();

    /// <summary>
    /// use GetitemsOnTable2 insted.
    /// </summary>
    [Obsolete] public Dictionary<SO_Item, int> GetItemsOnTable => _itemsOnTable;

    /// <summary>
    /// will be deleted
    /// </summary>
    [Obsolete] public Dictionary<SO_Item, int>.KeyCollection GetKeyCollectionOfItemsOnTable => _itemsOnTable.Keys; 
    public IReadOnlyDictionary<SO_Item, int> GetItemsOnTable2 => _itemsOnTable;

    public int GetItemTableValue(SO_Item value)
    {
        return _itemsOnTable[value];
    } 
    /// <summary>
    /// when crafting adding item to crafter is only limited to one
    /// </summary>
    public void OnCraftTable(SO_Item itemToAdd, List<SO_ItemBlueprint> bluePrints)
    {
        if (_itemsOnTable.ContainsKey(itemToAdd)) _itemsOnTable[itemToAdd]++;
        else _itemsOnTable.Add(itemToAdd, 1);
        print("-oncrafttable-");
        for (int i = 0; i < bluePrints.Count; i++)
        {
            var currentBPList = bluePrints[i].GetElements;
            for(int j = 0; j < currentBPList.Count; j++)
            {
                var item = currentBPList[j].so_item;
                if (!_itemsOnTable.ContainsKey(item)) return;
                if (_itemsOnTable[item] < currentBPList[j].amount) return;
            }
            //waht the fuck
            OnCraftSucessful();
            void OnCraftSucessful()
            {
                print("CRAFT_SUCCESSFUL ");
                foreach(var item2 in bluePrints[i].GetResult)
                {
                    print("item result name : " + item2.so_item + " amount : " + item2.amount);
                }
            }
        }
    }
    public void ClearItemsOnTable()
    {
        _itemsOnTable.Clear();
    }
    public void AddItemToCraft(SO_Item itemToAdd, int amount)
    {
        if (amount > 1) Debug.LogError("Fuck");
        InventoryItemVisual.UpdateItemVisCraft(itemToAdd);
    }
    public void OnCancelItem()
    {

    }
}

