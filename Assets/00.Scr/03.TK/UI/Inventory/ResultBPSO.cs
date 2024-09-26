using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "SO/Inventory/ResultBP")]
public class ResultBPSO : ScriptableObject
{
    public List<SO_Item> needItem = new();
    [SerializeField] private Sprite resultItemIcon;
    [SerializeField] private string resultItemName;
    [SerializeField] private string resultItemAmount;

    public int GetSize => needItem.Count;

    public Sprite GetSpriteAtIndex(int index)
    {
        if (needItem[index] != null)
        {
            return needItem[index].GetIcon;
        }
        else return null;
    }

    public int GetItemAmountAtIndex(int index)
    {
        if (needItem[index] != null)
        {
            return needItem[index].GetMaxAmount;
        }
        else return 0;
    }

    public string GetResultName() => resultItemName;
    public Sprite GetResultIcon() => resultItemIcon;
    public string GetResultAmount() => resultItemAmount;
}
