using DG.Tweening;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BlueprintViewer : MonoSingleton<BlueprintViewer>
{
    #region ������� �� �ڵ��
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
    ///currentBlueprint ���߿� List�� �����ͼ� �� ����
    ///�������� blueprint����Ʈ�� ������ �־�� ��.
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
        if (currentItem == null) //���� �������Ʈ�� ������� �� �־��ֱ�
        {
            Debug.Log("First item in");
            currentItem = newItem;

            currentItemSetting.Add(newItem.GetName, 1);
            itemTable.Add(newItem.GetName, newItem);
            SetCraftTable();
        }
        //���� �������Ʈ�� ���� �������Ʈ�� ���� �������Ʈ
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
        // ������ �������Ʈ�� ���� �������Ʈ�� �ٸ� ���
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

    // ���� ũ����Ʈ ���̺� ���� �ö� �������Ʈ�� ������Ʈ �����ش�.
    public void CreateBPOnCraftTable()
    {
        foreach (var unlockedBP in inventory.GetUnlockedBlueprints) //��ϵ� BP�� ��ȸ
        {
            foreach (var ingredient in unlockedBP.GetElement) //��ϵ� BP�� ���� ��ȸ
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

    //���� ũ����Ʈ ���̺� �ö� �������Ʈ�� �����ش�.
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
