using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintViewer : MonoBehaviour
{
    // 좀 걸리겠노 ㅠㅠ
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
        // 가능한 조합의 갯수는 현재의 블루프린트의 조합아이템의 갯수
        combinationAmount = currentItemBlueprint.GetResult.Count;

        // 새로운 아이템의 청사진과 조합아이템을 저장하는 리스트 초기화
        // 아이템 리스트에 아이템들 넣고, 결과 아이템 리스트에 결과 아이템 넣음
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

            Image icon = null; // 이거 나중에 item_so에서 이미지 반환하는거 만들면 그거씀
            string needItemAmount = combinationItemList[index].amount.ToString();
            string resultItemName = resultItemList[index].ToString(); // 이거도 이름 반환하는 거 만들어주..

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
