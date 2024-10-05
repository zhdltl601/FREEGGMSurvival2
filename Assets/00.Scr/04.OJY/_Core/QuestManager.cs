using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    [SerializeField] private Quest[] _mainQuest;
    private int _currentIndex = 0;
    public Quest GetCurrentQuest => _mainQuest[_currentIndex];
    protected override void Awake()
    {
        base.Awake();
        Inventory.OnItemChanged += HandleOnItemChanged;
        DontDestroyOnLoad(gameObject);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        Inventory.OnItemChanged -= HandleOnItemChanged;
    }
    private void HandleOnItemChanged(SO_Item arg1, int arg2)
    {
        bool r = IsCurrentQuestCompleted(InventoryUI.Instance.PlayerInventory.GetInventory);
        if(r)
        {
            _currentIndex++;
            if(_currentIndex == _mainQuest.Length)
            {
                print("completed all quest");
                Inventory.OnItemChanged -= HandleOnItemChanged;
            }
        }
    }

    public bool IsCurrentQuestCompleted(IReadOnlyDictionary<SO_Item, int> _inv)
    {
        bool result = false;
        var curQuest = GetCurrentQuest;
        var condition = curQuest.GetCondition;
        var Item = condition.GetItemStructs;
        //check Item
        if(condition.GetItemStructs.Length > 0)
        {
            foreach (var item in Item)
            {
                bool containsKey = _inv.ContainsKey(item.so_item);
                if (containsKey)
                {
                    bool isItemEnough = _inv[item.so_item] >= item.amount;
                    if (isItemEnough)
                    {
                        result = true;
                    }

                }
                return result;
            }
        }
        return result;
    }
}