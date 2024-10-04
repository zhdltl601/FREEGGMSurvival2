using DG.Tweening;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BlueprintViewer : MonoSingleton<BlueprintViewer>
{
    #region 현재까지 한 코드들
    [SerializeField] private Inventory inventory;

    [Header("Blueprint prefab")]
    public CraftItemBar craftItemPrefab;
    public BlueprintUI blueprintPrefab;

    [Header("Panels")]
    public CanvasGroup craftPanel;
    private RectTransform craftPanelTrm;
    private List<BlueprintUI> resultBlueprintList = new();

    [Tooltip("current BP && ItemVis Info")]
    ///<summary>
    ///currentBlueprint 나중에 List로 가져와서 다 띄우기
    ///아이템이 blueprint리스트를 가지고 있어야 함.
    ///</summary>
    private SO_Item currentItem;

    [Tooltip("Blueprints List")]
    private Dictionary<string, CraftItemBar> bpTable = new();
    public static Dictionary<string, int> currentItemSetting = new();
    public Dictionary<string, SO_Item> itemTable = new();

    [Tooltip("Child of parentOfBlueprints is set position autometicly by layout group")]
    private RectTransform parentOfBlueprints;

    private void Start()
    {
        parentOfBlueprints = GetComponent<RectTransform>()
                                .Find("BlueprintListLayout")
                                .GetComponent<RectTransform>();

        craftPanelTrm = craftPanel.transform
                            .Find("Layout").GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            RemoveBPOnCraftTable();
        }
    }

    public void SetCurrentItemBlueprint(SO_Item newItem)
    {
        if (currentItem == null) //현재 블루프린트가 없을경우 걍 넣어주기
        {
            Debug.Log("First item in");
            currentItem = newItem;

            currentItemSetting.Add(newItem.GetName, 1);
            itemTable.Add(newItem.GetName, newItem);
            SetCraftTable();
        }
        //들어온 블루프린트가 현재 블루프린트랑 같은 블루프린트
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
        // 현재의 블루프린트가 들어온 블루프린트와 다른 경우
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
        StringBuilder sb = new StringBuilder();

        foreach(var item in currentItemSetting)
        {
            CraftItemBar itemBar = Instantiate(craftItemPrefab, craftPanelTrm);
            sb.Append("x").Append(currentItemSetting[item.Key]);

            itemBar.SetUI(
                itemTable[item.Key].GetIcon,
                itemTable[item.Key].GetName,
                sb.ToString()
                );
        }
    }

    // 현재 크래프트 테이블 위에 올라간 블루프린트를 업데이트 시켜준다.
    public void CreateBPOnCraftTable()
    {
        foreach (var unlockedBP in inventory.GetUnlockedBlueprints) //언록된 BP들 순회
        {
            foreach (var ingredient in unlockedBP.GetElement) //언록된 BP의 재료들 순회
            {
                if (itemTable.ContainsKey(ingredient.so_item.GetName))
                {
                    CraftItemBar bp = Instantiate(craftItemPrefab, parentOfBlueprints);
                    SO_Item item = unlockedBP.GetResult[0].so_item;
                    bp.SetUI(item.GetIcon, item.GetName, item.GetMaxAmount.ToString());
                }
            }
        }
    } 
    #endregion

    //현재 크래프트 테이블에 올라간 블루프린트를 지워준다.
    public void RemoveBPOnCraftTable()
    {
        currentItem = null;

        foreach(var item in bpTable)
        {
            Destroy(item.Value.gameObject);
        }
        bpTable.Clear();
        currentItemSetting.Clear();
        itemTable.Clear();

        foreach(var item in resultBlueprintList)
        {
            Destroy(item.gameObject);
        }
        resultBlueprintList.Clear();
    }


    public void UnShowResultBPList()
    {
        foreach (var result in resultBlueprintList)
        {
            Destroy(result.gameObject);
        }

        craftPanel.DOFade(0, 0.5f).OnComplete(() =>
        {
            resultBlueprintList.Clear();
        });
    }
}
