using System;

/// <summary>
/// to use sudo dictionary serialized 
/// key(SO_Item) = so_item, amount(int) = value
/// </summary>
[Serializable]
public struct ItemStruct
{
    public SO_Item so_item;
    public int amount;
}