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

    public void SetCurrentItemBlueprint(SO_Item newItem)
    {
        if (currentItem == null) //current ingredient is null
        {
            Debug.Log("First item in");
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
                Debug.Log("Same item in");
                currentItemSetting[newItem.GetName]++;
                SetCraftTable();
            }
            else
                currentItemSetting.Add(newItem.GetName, 1);
        }
        //currentItem is different newitem
        else
        {
            Debug.Log("Different item in");
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
            Debug.Log("Doesn't set any item on crafting panel");
        }

        StringBuilder sb = new StringBuilder();

        RemoveCraftItemUI();

        foreach(var item in currentItemSetting.Keys)
        {
            sb.Clear();
            Debug.Log("Craft bar instantiate");
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

    // 현재 크래프트 테이블 위에 올라간 블루프린트를 업데이트 시켜준다.
    public void CreateBPOnCraftTable()
    {
        blueprintsPanel.DOFade(1, 0.2f);
        onCrafting = true;

        if(inventory.GetUnlockedBlueprints.Count == 0)
        {
            Debug.LogWarning("Any Blueprint doesn't unlocked!!");
            return;
        }

        foreach (var unlockedBP in inventory.GetUnlockedBlueprints) //언록된 BP들 순회
        {
            foreach (var ingredient in unlockedBP.GetElement) //언록된 BP의 재료들 순회
            {
                if (itemTable.ContainsKey(ingredient.so_item.GetName))
                {
                    BlueprintUI bp = Instantiate(blueprintPrefab, bpUILayout);
                    bp.SetUI(unlockedBP);
                    bpUIList.Add(bp);
                }
            }
        }
    } 

    //크래프팅 하고있는 모든 것을 다 지워줌
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
        bpUIList.Clear();
    }

    //BP들만 지워줌
    public void UnShowBPListUI()
    {
        onCrafting = false;
        blueprintsPanel.DOFade(0, 0f).OnComplete(() =>
        {
            DeleteBPList();
        });
    }

    public void DeleteBPList()
    {
        for(int i = 0; i < bpUIList.Count; i++)
        {
            Destroy(bpUIList[i].gameObject);
        }
        bpUIList.Clear();
    }
}
