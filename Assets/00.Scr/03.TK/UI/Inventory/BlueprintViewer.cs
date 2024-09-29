using DG.Tweening;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BlueprintViewer : MonoBehaviour
{
    [Header("Singleton Instance")]
    public static BlueprintViewer Instance;

    [Header("Blueprint prefab")]
    public CraftBlueprint blueprintPrefab;
    public ResultItemBP resultItemPrefab;

    [Header("Panels")]
    public CanvasGroup craftPanel;
    private RectTransform craftPanelTrm;
    private List<ResultItemBP> resultBlueprintList = new();

    [Tooltip("current BP && ItemVis Info")]
    ///<summary>
    ///currentBlueprint ���߿� List�� �����ͼ� �� ����
    ///�������� blueprint����Ʈ�� ������ �־�� ��.
    ///</summary>
    private SO_ItemBlueprint currentBlueprint;

    [Tooltip("Blueprints List")]
    private Dictionary<string, CraftBlueprint> bpTable = new();
    public static Dictionary<string, int> currentBPSetting = new();
    public Dictionary<string, SO_Item> itemTable = new();

    [Tooltip("Child of parentOfBlueprints is set position autometicly by layout group")]
    private RectTransform parentOfBlueprints;
    
    private void Awake()
    {
        //������ �̱���
        if (Instance != null) Destroy(Instance.gameObject);

        Instance ??= this;
    }

    private void Start()
    {
        //�������Ʈ�� ����
        parentOfBlueprints =
            GetComponent<RectTransform>()
            .Find("BlueprintListLayout")
            .GetComponent<RectTransform>();

        craftPanelTrm = craftPanel.transform
            .Find("Layout").GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            RemoveBPOnCraftTable();
        }
    }

    public void SetCurrentItemBlueprint(SO_ItemBlueprint blueprint)
    {
        SO_Item ingredientItem = null;
        if (currentBlueprint == null) //���� �������Ʈ�� ������� �� �־��ֱ�
        {
            currentBlueprint = blueprint;
            ingredientItem = currentBlueprint.GetElement[0].so_item;

            currentBPSetting.Add(ingredientItem.GetName, 1);
            itemTable.Add(ingredientItem.GetName, ingredientItem);
            CreateBPOnCraftTable();
        }
        //���� �������Ʈ�� ���� �������Ʈ�� ���� �������Ʈ
        else if (currentBlueprint.GetElement[0].so_item.GetName
            == blueprint.GetElement[0].so_item.GetName) 
        {
            if (currentBPSetting.ContainsKey(currentBlueprint.GetElement[0].so_item.GetName))
            {
                print("Same item input");
                currentBPSetting[currentBlueprint.GetElement[0].so_item.GetName]++;
                CreateBPOnCraftTable();
            }
        }
        // ������ �������Ʈ�� ���� �������Ʈ�� �ٸ� ���
        else
        {
            currentBlueprint = blueprint;

            if (currentBPSetting.ContainsKey(currentBlueprint.GetElement[0].so_item.GetName))
            {
                currentBPSetting[currentBlueprint.GetElement[0].so_item.GetName]++;
            }
            else
            {
                currentBPSetting.Add(currentBlueprint.GetElement[0].so_item.GetName, 1);
                itemTable.Add(currentBlueprint.GetElement[0].so_item.GetName, 
                    currentBlueprint.GetElement[0].so_item);
            }
            CreateBPOnCraftTable();
        }
    }

    // ���� ũ����Ʈ ���̺� ���� �ö� �������Ʈ�� ������Ʈ �����ش�.
    public void CreateBPOnCraftTable()
    {
        StringBuilder sb = new StringBuilder();

        foreach(var item in currentBPSetting)
        {
            CraftBlueprint bp = null;

            if(!bpTable.ContainsKey(item.Key))
            {
                bp = Instantiate(blueprintPrefab, parentOfBlueprints);

                sb.Clear();
                sb.Append("x").Append($"{item.Value}");

                bp.SetUI(itemTable[item.Key].GetIcon, item.Key, "1");
                bpTable.Add(item.Key, bp);
            }
            else
            {
                sb.Clear();
                sb.Append("x").Append($"{item.Value}");

                bp = bpTable[item.Key];
                bp.SetUI(itemTable[item.Key].GetIcon, item.Key, sb.ToString());
            }
        }
    }

    //���� ũ����Ʈ ���̺� �ö� �������Ʈ�� �����ش�.
    public void RemoveBPOnCraftTable()
    {
        currentBlueprint = null;

        foreach(var item in bpTable)
        {
            Destroy(item.Value.gameObject);
        }
        bpTable.Clear();
        currentBPSetting.Clear();
        itemTable.Clear();

        foreach(var item in resultBlueprintList)
        {
            Destroy(item.gameObject);
        }
        resultBlueprintList.Clear();
    }

    public void ShowResultBPList()
    {
        if (currentBlueprint == null) return;

        craftPanel.gameObject.SetActive(true);
        craftPanel.DOFade(1, 0.5f);

        ResultItemBP bp = Instantiate(resultItemPrefab, craftPanelTrm);
        resultBlueprintList.Add(bp);

        bp.SetUI(currentBlueprint);
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
