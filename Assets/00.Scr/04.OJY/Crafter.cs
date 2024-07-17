using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Crafter : MonoBehaviour
{
    private readonly Dictionary<SO_Item, int> _itemsOnTable = new();
    public Dictionary<SO_Item, int> GetItemsOnTable => _itemsOnTable;
    //unfinished 
    public void OnCraftTable(SO_Item itemToAdd, List<SO_ItemBlueprint> bluePrints)
    {
        if (_itemsOnTable.ContainsKey(itemToAdd)) _itemsOnTable[itemToAdd]++;
        else _itemsOnTable.Add(itemToAdd, 1);

        print("-oncrafttable-");
        for (int i = 0; i < bluePrints.Count; i++)
        {
            var currentBPList = bluePrints[i].GetList;
            for (int j = 0; j < currentBPList.Count; j++)
            {
                if (!_itemsOnTable.ContainsKey(currentBPList[j].so_item)) return;
                if (_itemsOnTable[currentBPList[j].so_item] < currentBPList[j].amount) return;
            }
            void OnCraftSucessful()
            {
                this.OnCraftSucessful();
                //print(c)
            }
            OnCraftSucessful();
        }
    }
    private void OnCraftSucessful()
    {
        Debug.Log("craft sucessful");
    }
}
