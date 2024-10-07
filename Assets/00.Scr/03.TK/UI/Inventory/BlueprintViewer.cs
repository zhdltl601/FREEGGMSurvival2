using DG.Tweening;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BlueprintViewer : MonoSingleton<BlueprintViewer>
{
    [SerializeField] private Inventory inventory;

    [Header("Blueprint prefab")]
    public CraftItemBar craftItemPrefab;
    public BlueprintUI blueprintPrefab;

    [Header("Panels")]
    public CanvasGroup blueprintsPanel;
    [SerializeField] private RectTransform craftItemUILayout;
    [SerializeField] private RectTransform bpUILayout;
    private List<BlueprintUI> bpUIList = new();
    private List<CraftItemBar> craftItemList = new();

    private SO_Item currentItem = null;

    [Tooltip("Blueprints List")]
    private Dictionary<string, CraftItemBar> bpTable = new();
    public Dictionary<string, int> currentItemSetting = new();
    public Dictionary<string, SO_Item> itemTable = new();

    public bool onCrafting = false;

    private void Start()
    {
        currentItem = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            RemoveAllByCrafting();
        }
    }

    public void OnCraftTable(SO_Item newItem)
    {
        if (currentItem == null) //current ingredient is null
        {
            currentItem = newItem;

            currentItemSetting.Add(newItem.GetName, 1);
            itemTable.Add(newItem.GetName, newItem);
            SetCraftTable();
        }
        //In ingredient is same new ingredient
        else if (currentItem.GetName == newItem.GetName)
        {
            if (currentItemSetting.ContainsKey(newItem.GetName))
            {
                currentItemSetting[newItem.GetName]++;
            }
            else
                currentItemSetting.Add(newItem.GetName, 1);
            SetCraftTable();
        }
        //currentItem is different newitem
        else
        {
            currentItem = newItem;

            if (currentItemSetting.ContainsKey(newItem.GetName))
            {
                currentItemSetting[newItem.GetName]++;
            }
            else
            {
                currentItemSetting.Add(newItem.GetName, 1);
                itemTable.Add(newItem.GetName, newItem);
            }
            SetCraftTable();
        }
    }
    public void SetCraftTable()
    {
        if(currentItemSetting.Count == 0)
        {
            return;
        }

        StringBuilder sb = new StringBuilder();

        RemoveCraftItemUI();

        foreach(var item in currentItemSetting.Keys)
        {
            sb.Clear();
            CraftItemBar itemBar = Instantiate(craftItemPrefab, craftItemUILayout);
            sb.Append("x").Append(currentItemSetting[item]);

            itemBar.SetUI(itemTable[item].GetIcon, itemTable[item].GetName, sb.ToString());
            craftItemList.Add(itemBar);
        }
    }
    private void RemoveCraftItemUI()
    {
        if (craftItemList.Count == 0) return;

        for (int i = 0; i < craftItemList.Count; i++)
        {
            Destroy(craftItemList[i].gameObject);
        }
        craftItemList.Clear();
    }

    // ���� ũ����Ʈ ���̺� ���� �ö� �������Ʈ�� ������Ʈ �����ش�.
    public void CreateBPOnCraftTable()
    {
        blueprintsPanel.DOFade(1, 0f);

        if(inventory.GetUnlockedBlueprints.Count == 0)
        {
            Debug.LogWarning("Any blueprint doesn't unlocked!!");
            return;
        }

        DeleteBPList();

        foreach (var unlockedBP in inventory.GetUnlockedBlueprints) //��ϵ� BP�� ��ȸ
        {
            foreach (var ingredient in unlockedBP.GetElement) //��ϵ� BP�� ���� ��ȸ
            {
                if (currentItemSetting.ContainsKey(ingredient.so_item.GetName))
                {
                    BlueprintUI bp = Instantiate(blueprintPrefab, bpUILayout);
                    bp.SetUI(unlockedBP);
                    bpUIList.Add(bp);
                }
            }
        }
        onCrafting = true;
    } 

    //ũ������ �ϰ��ִ� ��� ���� �� ������
    public void RemoveAllByCrafting()
    {
        currentItem = null;

        RemoveCraftItemUI();
        foreach(var item in bpTable)
        {
            Destroy(item.Value.gameObject);
        }
        bpTable.Clear();
        currentItemSetting.Clear();
        itemTable.Clear();

        UnShowBPListUI();
    }

    //BP�鸸 ������
    public void UnShowBPListUI()
    {
        blueprintsPanel.DOFade(0, 0f).OnComplete(() =>
        {
            DeleteBPList();
        });
        onCrafting = false;
    }

    public void DeleteBPList()
    {
        if(bpUIList.Count == 0) return;

        for(int i = 0; i < bpUIList.Count; i++)
        {
            Destroy(bpUIList[i].gameObject);
        }
        bpUIList.Clear();
    }
}
