using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    private readonly Dictionary<SO_Item, int> _inventory = new();
    private static readonly List<SO_ItemBlueprint> _unlockedBlueprints = new();
    public static List<SO_ItemBlueprint> GetUnlockedBluePrints => _unlockedBlueprints;
    public static void AddBluePrint(SO_ItemBlueprint bpToAdd)
    {
        if (!_unlockedBlueprints.Contains(bpToAdd)) _unlockedBlueprints.Add(bpToAdd);
    }
    /// <summary>
    /// this function needs improvement(optimazation:dictionary seraching)
    /// </summary>
    public bool TryAddItemToInventory(SO_Item itemToAdd, int amount = 1)
    {
        bool result;
        if (!_inventory.ContainsKey(itemToAdd))
        {
            result = true;
            _inventory.Add(itemToAdd, amount);
        }
        else
        {
            result = _inventory[itemToAdd] + amount <= itemToAdd.GetMaxAmount;
        }
        if(result) _inventory[itemToAdd] += amount;
        return result;
    }

    public void PrintShit()
    {
        print("-inv-");
        foreach(var a in _inventory)
        {
            print(a.Key + " " + a.Value);
        }
        print("-bp-");
        foreach(var a in _unlockedBlueprints)
        {
            print(a);
        }
    }
    /// <summary>
    /// this function needs improvement(exception:key not found)
    /// </summary>
    public bool TryAddItemToCraft(SO_Item itemToAdd, Crafter crafter, List<SO_ItemBlueprint> unlockedBlueprints = null)
    {
        unlockedBlueprints = unlockedBlueprints is null ? _unlockedBlueprints : unlockedBlueprints;
        void AddItemToCraft()
        {
            _inventory[itemToAdd]--;
            crafter.OnCraftTable(itemToAdd, unlockedBlueprints);
        }

        bool result = false;
        if(_inventory[itemToAdd] > 0)//exception
        {
            AddItemToCraft();
            result = true;
        }
        return result;
    }
    public void RemoveAllItemToCraft(Crafter crafter)
    {
        var inventory = crafter.GetItemsOnTable;
        //resotre all items from table
        foreach (var item in inventory)
        {
            _inventory[item.Key] += inventory[item.Key];
        }
        inventory.Clear();
    }
    public void CraftItem()
    {

    }
}
