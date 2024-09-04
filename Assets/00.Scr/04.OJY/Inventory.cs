using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    private readonly Dictionary<SO_Item, int> _inventory = new();
    private static readonly List<SO_ItemBlueprint> _unlockedBlueprints = new();

    public IReadOnlyDictionary<SO_Item, int> GetInventory => _inventory;

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
        #region Exception
        int itemMaxAmount = itemToAdd.GetMaxAmount;
        if (amount < 1)
        {
            Debug.LogError("add amount is small than \'1\'");
            return false;
        }
        bool firstInit = !_inventory.ContainsKey(itemToAdd);
        if(!firstInit && _inventory[itemToAdd] >= itemMaxAmount)
        {
            Debug.LogError("item already full or over");
            return false;
        }
        #endregion
        bool isCapOver = !firstInit && //if it's first init, no need to check
            _inventory[itemToAdd] + amount > itemMaxAmount;
        bool result = false;  

        if (!isCapOver)
        {
            if (firstInit) _inventory.Add(itemToAdd, amount);
            else _inventory[itemToAdd] += amount;
            OnItemChanged?.Invoke(itemToAdd, amount);
            result = true;
        }
        else
        {
            int val = itemMaxAmount - _inventory[itemToAdd];
            if (firstInit)
            {
                _inventory.Add(itemToAdd, itemMaxAmount);
                OnItemChanged?.Invoke(itemToAdd, val);
                result = true;
            }
            else if(amount != 1 && allowOverTake)
            {
                _inventory[itemToAdd] = itemMaxAmount;
                OnItemChanged?.Invoke(itemToAdd, val);
                result = true;
            }
        }
        return result;
    }
    public bool TrySubtractItemToInventory(SO_Item itemToSubtract, int amount = 1, bool allowOverTake = true)
    {
        #region Exception
        if (amount < 1)
        {
            Debug.LogError("add amount is small than \'1\'");
            return false;
        }
        bool firstInit = !_inventory.ContainsKey(itemToSubtract);
        if (!firstInit && _inventory[itemToSubtract] <= 0)
        {
            Debug.LogError("item already 0 or lower");
            return false;
        }
        #endregion

        bool result = false;
        if (!firstInit)
        {
            bool isCapNotOver = _inventory[itemToSubtract] - amount >= 0;
            if (isCapNotOver)
            {
                _inventory[itemToSubtract] -= amount;
                OnItemChanged?.Invoke(itemToSubtract, -amount);
                result = true;
            }
            else if(!isCapNotOver && amount != 1 && allowOverTake)
            {
                int val = _inventory[itemToSubtract];
                _inventory[itemToSubtract] = 0;
                print("fuck" + (itemToSubtract.GetMaxAmount - val));
                OnItemChanged?.Invoke(itemToSubtract, -val);
                result = true;
            }
        }
        return result;
    }
    #region crafter //can remove Crafter parameter
    public bool TryAddItemToCraft(SO_Item itemToAdd, Crafter crafter, IReadOnlyList<SO_ItemBlueprint> unlockedBlueprints = null)
    {
        //todo : remove this and make separate function that check if player can craft stuff 
        unlockedBlueprints = unlockedBlueprints is null ? _unlockedBlueprints : unlockedBlueprints; // to check if it's possible to craft one. set ubp(3) null for most cases 

        bool result = _inventory.ContainsKey(itemToAdd) && _inventory[itemToAdd] > 0;
        if(result)
        {
            void AddItemToCraft()
            {
                TrySubtractItemToInventory(itemToAdd, 1);
                crafter.OnCraftTable(itemToAdd, unlockedBlueprints);
            }
            AddItemToCraft();
        }
        return result;
    }
    /// <summary>
    /// cancel crafting. resotre all items from table
    /// </summary>
    public void CancelCraft(Crafter crafter)
    {
        var inventory = crafter.GetItemsOnTable.Keys;
        foreach (var item in inventory)
        {
            int amount = crafter.GetItemsOnTable[item];
            _inventory[item] += amount;
            OnItemChanged?.Invoke(item, amount);
        }
        crafter.ClearItemsOnTable();
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