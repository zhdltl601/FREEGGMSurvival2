using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct QuestCondition
{
    [SerializeField] private ItemStruct[] _itemList;
    public ItemStruct[] GetItemStructs => _itemList;
}
