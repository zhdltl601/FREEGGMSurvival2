using System.Collections.Generic;
using UnityEngine;
public class InventoryItemVisual : MonoBehaviour
{
    [SerializeField] private SO_Item _soItem;
    [SerializeField] private SO_ItemBlueprint _soItemBlueprint;
    public SO_ItemBlueprint GetSO_ItemBlueprint => _soItemBlueprint;
    public SO_Item GetSO_Item => _soItem;//
    private static readonly Dictionary<SO_Item, List<GameObject>> _visualDictionary = new();
    public static void UpdateItemVisAdd(SO_Item itemToAdd, int amount)
    {
        print("vuADD");
        bool firstInit = !_visualDictionary.ContainsKey(itemToAdd);
        if (firstInit)
        {
            void Init()
            {
                _visualDictionary.Add(itemToAdd, new());
                var list = _visualDictionary[itemToAdd];
                for(int i = 0; i < itemToAdd.GetVisPosInv.Count; i++)
                {
                    list.Add(null);
                }
            }
            Init();
        }
        var list = _visualDictionary[itemToAdd];
        bool isIncreassing = amount > 0;
        int firstNullIndex = GetFirstNullIndex(list);

        int target = firstNullIndex + amount;
        if (isIncreassing)
        {
            for(int i = firstNullIndex; i < target; i++)
            {
                bool isNull = list[i] == null;
                if (isNull) list[i] = CreateVis(i, itemToAdd);
                list[i].SetActive(true);
            }
        }
        else
        {
            for(int i = firstNullIndex - 1; i >= target; i--)
            {
                list[i].SetActive(false);
            }
        }
    }
    public static void UpdateItemVisSet(SO_Item itemToSet, int value)
    {
        print("vuSet");
        bool firstInit = !_visualDictionary.ContainsKey(itemToSet);
        if (firstInit)
        {
            void Init()
            {
                _visualDictionary.Add(itemToSet, new());
                var list = _visualDictionary[itemToSet];
                for (int i = 0; i < itemToSet.GetVisPosInv.Count; i++)
                {
                    list.Add(null);
                }
            }
            Init();
        }
        var list = _visualDictionary[itemToSet];
        int firstNullIndex = GetFirstNullIndex(list);
        bool isIncreassing = value > firstNullIndex;

        int target = value;
        if (isIncreassing)
        {
            for (int i = firstNullIndex; i < target; i++)
            {
                bool isNull = list[i] == null;
                if (isNull) list[i] = CreateVis(i, itemToSet);
                list[i].SetActive(true);
            }
        }
        else
        {
            for (int i = firstNullIndex - 1; i >= target; i--)
            {
                list[i].SetActive(false);
            }
        }
    }
    public void SelectObj(SO_ItemBlueprint bp)
    {
        ItemInfoPanel.Instance.DisableInfoPanel();
        BlueprintViewer.Instance.SetCurrentItemBlueprint(bp);
    }

    public void SetItemInfoPanelOn(SO_Item soItem)
    {
        ItemInfoPanel.Instance.SetItemInfoPanel(soItem.GetIcon,
            soItem.GetName, soItem.GetMaxAmount.ToString());
    }

    public void DestroyThisObj()
    {
        Destroy(this.gameObject);
    }

    private static int GetFirstNullIndex(List<GameObject> list)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            if (list[i] == null || !list[i].activeSelf) return i;
        }
        return list.Count;
    }
    private static GameObject CreateVis(int index, SO_Item itemToCreate)
    {
        GameObject prefab = itemToCreate.GetPrefab;
        Quaternion quaternion = Quaternion.identity;
        Transform spawnParent = InventoryUI.Instance._piv;
        Vector3 pos = itemToCreate.GetVisPosInv[index] + spawnParent.position;
        GameObject newInstance = Instantiate(prefab, pos, quaternion, parent: spawnParent);
        return newInstance;
    }
}
