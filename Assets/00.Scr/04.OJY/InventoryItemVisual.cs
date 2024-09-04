using System.Collections.Generic;
using UnityEngine;
public class InventoryItemVisual : MonoBehaviour
{
    [SerializeField] private SO_Item _soItem;
    public SO_Item GetSO_Item => _soItem;//
    private static readonly Dictionary<SO_Item, List<GameObject>> _visualDictionary = new();
    /// <summary>
    /// expectes p:amount calculated
    /// p:adds amount (pos + amount)
    /// <para>_pos.Count + p lower than _soItem.maxamount + 1</para>
    /// <para>_pos.Count + p upper than 0 + 1</para>
    /// </summary>
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
        int firstNullIndex = GetFirstNullIndex();

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
            Vector3 pos = itemToAdd.GetVisPosInv[index];
            Quaternion quaternion = Quaternion.identity;
Transform spawnPos = DebugUI.Instance._piv;//

            GameObject newInstance = Instantiate(prefab, pos, quaternion, parent : spawnPos);
            list[index] = newInstance;
        }
        int target = firstNullIndex + amount;
        if (isIncreassing)
        {
            for(int i = firstNullIndex; i < target; i++)
            {
                bool isNull = list[i] == null;
                if (isNull) CreateVis(i);
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
        int firstNullIndex = GetFirstNullIndex();
        bool isIncreassing = value > firstNullIndex;

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
            GameObject prefab = itemToSet.GetPrefab;
            Vector3 pos = itemToSet.GetVisPosInv[index];
            Quaternion quaternion = Quaternion.identity;
Transform spawnPos = DebugUI.Instance._piv;//

            GameObject newInstance = Instantiate(prefab, pos, quaternion, parent: spawnPos);
            list[index] = newInstance;
        }
        int target = value;
        if (isIncreassing)
        {
            for (int i = firstNullIndex; i < target; i++)
            {
                bool isNull = list[i] == null;
                if (isNull) CreateVis(i);
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
}
