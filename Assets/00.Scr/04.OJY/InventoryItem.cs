using System.Collections.Generic;
using UnityEngine;
public class InventoryItem : MonoBehaviour
{
    [SerializeField] private SO_Item _soItem;
    public SO_Item GetSO_Item => _soItem;
    private static readonly Dictionary<SO_Item, List<GameObject>> _visualDictionary = new();
    /// <summary>
    /// expectes p:amount calculated
    /// <para>_pos.Count + p lower than _soItem.maxamount + 1</para>
    /// <para>_pos.Count + p upper than 0 + 1</para>
    /// </summary>
    public static void UpdateItemVisual(SO_Item itemToAdd, int amount)
    {
        print("visUpdate");
        if (!_visualDictionary.ContainsKey(itemToAdd))
        {
            void CreateInstance()
            {
                _visualDictionary.Add(itemToAdd, new());
                var list = _visualDictionary[itemToAdd];
                for(int i = 0; i < itemToAdd.GetMaxAmount; i++)
                {
                    list.Add(null);
                }
            }
            CreateInstance();
        }
        var list = _visualDictionary[itemToAdd];
        bool isIncreassing = amount > 0;
        int index = GetFirstNullIndex();

        int GetFirstNullIndex()
        {
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                if (list[i] == null || !list[i].activeSelf) return i;
            }
            return list.Count;
        }
        void CreateVis(int index)
        {
            GameObject prefab = itemToAdd.GetPrefab;
            Vector3 pos = itemToAdd.GetPos[index];
            Quaternion quaternion = Quaternion.identity;
Transform spawnPos = DebugUI.Instance._piv;//

            GameObject newInstance = Instantiate(prefab, pos, quaternion, parent : spawnPos);
            list[index] = newInstance;
        }
        int target = index + amount;
int collectionMaxAmount = itemToAdd.GetPosMaxCount;
        if (isIncreassing)
        {
            for(int i = index; i < target; i++)
            {
//if (i >= collectionMaxAmount) break;
                bool isNull = list[i] == null;
                if (isNull) CreateVis(i);
                list[i].SetActive(true);
            }
        }
        else
        {
            for(int i = index - 1; i >= target; i--)
            {
                list[i].SetActive(false);
            }
        }

    }
}
