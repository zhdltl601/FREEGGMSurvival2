using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Crafter : MonoBehaviour
{
    private readonly Dictionary<SO_Item, int> _itemsOnTable = new();

    public IReadOnlyDictionary<SO_Item, int> GetItemsOnTable => _itemsOnTable;

    /// <summary>
    /// when crafting adding item to crafter is only limited to one
    /// </summary>
    public void OnCraftTable(SO_Item itemToAdd, IReadOnlyList<SO_ItemBlueprint> bluePrints)
    {
        if (_itemsOnTable.ContainsKey(itemToAdd)) _itemsOnTable[itemToAdd]++;
        else _itemsOnTable.Add(itemToAdd, 1);
        print("-oncrafttable-");
        for (int i = 0; i < bluePrints.Count; i++)
        {
            var currentBPList = bluePrints[i].GetElement;
            for(int j = 0; j < currentBPList.Count; j++)
            {
                var item = currentBPList[j].so_item;
                if (!_itemsOnTable.ContainsKey(item)) return;
                if (_itemsOnTable[item] < currentBPList[j].amount) return;
            }
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
}
