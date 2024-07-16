using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Crafter : MonoBehaviour
{
    private readonly Dictionary<SO_Item, int> itemsOnTable = new();
    public Dictionary<SO_Item, int> GetItemsOnTable => itemsOnTable;
    public void OnCraftTable(SO_Item itemToAdd, List<SO_ItemBlueprint> bluePrints)
    {
        //itemsOnTable.TryAdd(itemToAdd.item.so_item, 1);
        if (itemsOnTable.ContainsKey(itemToAdd)) itemsOnTable[itemToAdd]++;
        else itemsOnTable.Add(itemToAdd, 1);

        print("-oncrafttable-");
        for (int i = 0; i < bluePrints.Count; i++)
        {
            var currentBPList = bluePrints[i].GetList;
            for (int j = 0; j < currentBPList.Count; j++)
            {
                if (!itemsOnTable.ContainsKey(currentBPList[j].so_item)) return;
                if (itemsOnTable[currentBPList[j].so_item] < currentBPList[j].amount) return;
            }
            OnCraftSucessful();
        }
    }
    private void OnCraftSucessful()
    {
        Debug.Log("no tfuck");
    }
}
