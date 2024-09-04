using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintViewer : MonoBehaviour
{
    // �� �ɸ��ڳ� �Ф�
    public CraftBlueprint blueprintPrefab;
    private SO_ItemBlueprint currentItemBlueprint = null;
    private RectTransform parentOfBlueprints;

    private int combinationAmount = 0;

    private List<ItemStruct> combinationItemList = new();
    private List<ItemStruct> resultItemList = new();
    private List<CraftBlueprint> blueprintList = new();

    private void Start()
    {
        parentOfBlueprints =
            GetComponent<RectTransform>()
            .Find("BlueprintListLayout")
            .GetComponent<RectTransform>();
    }

    public void SetCurrentItemBlueprint(SO_ItemBlueprint newBlueprint)
    {
        if (currentItemBlueprint == newBlueprint) return;

        RemoveBlueprintList();

        combinationItemList.Clear();
        resultItemList.Clear();

        currentItemBlueprint = newBlueprint;
        // ������ ������ ������ ������ �������Ʈ�� ���վ������� ����
        combinationAmount = currentItemBlueprint.GetResult.Count;

        // ���ο� �������� û������ ���վ������� �����ϴ� ����Ʈ �ʱ�ȭ
        // ������ ����Ʈ�� �����۵� �ְ�, ��� ������ ����Ʈ�� ��� ������ ����
        combinationItemList = currentItemBlueprint.GetElements;
        resultItemList = currentItemBlueprint.GetResult;

        if (combinationItemList.Count != resultItemList.Count)
            print("Combi item and result item count is different");

        UpdateCraftTable();
    }

    public void UpdateCraftTable()
    {
        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < combinationAmount; index++)
        {
            CraftBlueprint bp = Instantiate(blueprintPrefab, parentOfBlueprints);
            blueprintList.Add(bp);

            Image icon = null; // �̰� ���߿� item_so���� �̹��� ��ȯ�ϴ°� ����� �װž�
            string needItemAmount = combinationItemList[index].amount.ToString();
            string resultItemName = resultItemList[index].ToString(); // �̰ŵ� �̸� ��ȯ�ϴ� �� �������..

            sb.Clear();
            sb.Append("x");
            sb.Append(needItemAmount);

            bp.SetUI(icon, sb.ToString(), resultItemName);
        }
    }

    private void RemoveBlueprintList()
    {
        if (combinationAmount == 0) return;

        for (int index = 0; index < combinationAmount; index++)
        {
            CraftBlueprint bp = blueprintList[index];
            Destroy(bp.gameObject);
        }
        blueprintList.Clear();
    }
}
