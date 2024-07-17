using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    private readonly Dictionary<SO_Item, int> _inventory = new();
    private static readonly List<SO_ItemBlueprint> _unlockedBlueprints = new();
    public Dictionary<SO_Item, int> GetInventory => _inventory;
    #region Events
    public static event Action<SO_Item, int> OnItemChanged; // only call this function after calculation
    //public static event Action<SO_ItemBlueprint, int> OnBPChanged;
    #endregion
    public static void AddBluePrint(SO_ItemBlueprint bpToAdd)
    {
        if (!_unlockedBlueprints.Contains(bpToAdd)) _unlockedBlueprints.Add(bpToAdd);
    }

    public bool TryAddItemToInventory(SO_Item itemToAdd, int amount = 1, bool allowOverTake = true)
    {
        // i(optimazation:dictionary seraching) and
        // amount can be negative value
        if (amount < 1)
        {
            Debug.LogError("add amount is small than \'1\'");
            return false;
        }
        bool firstInit = !_inventory.ContainsKey(itemToAdd);
        if(!firstInit && _inventory[itemToAdd] >= itemToAdd.GetMaxAmount)
        {
            Debug.LogError("item already full or over");
            return false;
        }

        bool isNotOver = !firstInit && _inventory[itemToAdd] + amount <= itemToAdd.GetMaxAmount;
        bool result = firstInit ||
                      isNotOver;
        if (result)
        {
            if (firstInit)
                _inventory.Add(itemToAdd, amount);
            else
                _inventory[itemToAdd] += amount;
            OnItemChanged(itemToAdd, amount);
        }
        else if (!isNotOver && amount != 1 && allowOverTake)
        {
            result = true;
            int val = itemToAdd.GetMaxAmount - _inventory[itemToAdd];
            _inventory[itemToAdd] += val;
            print("val" + val);
            OnItemChanged(itemToAdd, val);
        }

        return result;
    }
    public bool TrySubtractItemToInventory(SO_Item itemToSubtract, int amount = 1, bool allowOverTake = true)
    {
        // i(amount can be negative value)
        if (amount < 1)
        {
            Debug.LogError("add amount is small than \'1\'");
            return false;
        }
        bool result = _inventory.ContainsKey(itemToSubtract);
        if (result && _inventory[itemToSubtract] <= 0)
        {
            Debug.LogError("item already 0 or lower");
            return false;
        }
        if (result)
        {
            bool isNotOver = _inventory[itemToSubtract] - amount >= 0;
            result &= isNotOver;
            if (result)
            {
                _inventory[itemToSubtract] -= amount;
                OnItemChanged(itemToSubtract, -amount);
            }
            if(!isNotOver && amount != 1 && allowOverTake)
            {
                result = true;
                int val = _inventory[itemToSubtract];
                print("val" + val);
                _inventory[itemToSubtract] -= val;
                OnItemChanged(itemToSubtract, val);
            }
        }
        return result;
    }
    #region crafter //can remove Crafter parameter
    public bool TryAddItemToCraft(SO_Item itemToAdd, Crafter crafter, List<SO_ItemBlueprint> unlockedBlueprints = null)
    {
        // <para>adds item to table
        // i(exception:key not found)
        unlockedBlueprints = unlockedBlueprints is null ? _unlockedBlueprints : unlockedBlueprints;
        void AddItemToCraft()
        {
            _inventory[itemToAdd]--;
            crafter.OnCraftTable(itemToAdd, unlockedBlueprints);
            OnItemChanged?.Invoke(itemToAdd, -1);
        }
        bool result = false;
        if(_inventory[itemToAdd] > 0)//exception
        {
            AddItemToCraft();
            result = true;
        }
        return result;
    }
    /// <summary>
    /// cancel crafting. resotre all items from table
    /// </summary>
    public void CancelCraft(Crafter crafter)
    {
        var inventory = crafter.GetItemsOnTable;
        foreach (var item in inventory.Keys)
        {
            int amount = crafter.GetItemsOnTable[item];
            _inventory[item] += amount;
            OnItemChanged?.Invoke(item, amount);
        }
        inventory.Clear();
    }
    public void TryCraftItem()//unfin
    {
        void CraftItem()
        {
            print("Crafted Item");

        }
        bool canCraft = false;
        if (canCraft) CraftItem();

    }
    #endregion
    #region Debug
    public void Debug_PrintShit()
    {
        print("-inv-");
        foreach (var a in _inventory)
        {
            print(a.Key + " " + a.Value);
        }
        print("-bp-");
        foreach (var a in _unlockedBlueprints)
        {
            print(a);
        }
    }
    #endregion
}