using DG.Tweening;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlueprintUI : MonoBehaviour, IPointerDownHandler
{
    public Image[] images;
    public TextMeshProUGUI[] ingredientAmount;

    [SerializeField] private Image resultItemIcon;
    [SerializeField] private TextMeshProUGUI resultItemName;
    [SerializeField] private TextMeshProUGUI resultItemAmount;
    private SO_Item resultItem;
    private Inventory inventory;

    public bool canCraft = false;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void SetUI(SO_ItemBlueprint itemInfo)
    {
        images = transform
            .Find("ResultIcons").GetComponentsInChildren<Image>();
        ingredientAmount = transform
            .Find("ResultAmounts").GetComponentsInChildren<TextMeshProUGUI>();

        IngrediantSetting(itemInfo);

        resultItem = itemInfo.GetResult[0].so_item;

        resultItemIcon.sprite = itemInfo.GetResult[0].so_item.GetIcon;
        resultItemName.text = itemInfo.GetResult[0].so_item.GetName;
        resultItemAmount.text = itemInfo.GetResult[0].so_item.GetMaxAmount.ToString();
    }

    private void IngrediantSetting(SO_ItemBlueprint itemInfo)
    {
        for (int i = 0; i < itemInfo.GetElement.Count; i++)
        {
            Sprite icon = itemInfo.GetElement[i].so_item.GetIcon;
            if (icon != null)
            {
                images[i].sprite = icon;
                images[i].DOFade(1, 0.1f);
            }
            else
            {
                images[i].sprite = null;
                images[i].DOFade(0, 0.1f);
            }
        }

        StringBuilder sb = new StringBuilder();

        bool canComb = true;

        for (int j = 0; j < itemInfo.GetElement.Count; j++)
        {
            sb.Clear();
            int currentAmount = 0;
            if (BlueprintViewer.currentItemSetting
                .ContainsKey(itemInfo.GetElement[j].so_item.GetName))
            {
                currentAmount =
                    BlueprintViewer.currentItemSetting[itemInfo.GetElement[j].so_item.GetName];
            }
            sb.Append(currentAmount.ToString());
            sb.Append("/");
            int needAmount = itemInfo.GetElement[j].amount;
            sb.Append(needAmount);
            if (needAmount != 0)
                ingredientAmount[j].text = sb.ToString();
            else
                ingredientAmount[j].text = "";

            if(currentAmount < needAmount)
                canComb = false;
        }

        canCraft = canComb;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canCraft)
        {
            inventory.TryAddItemToInventory(resultItem);
            BlueprintViewer.Instance.RemoveBPOnCraftTable();
            Debug.Log($"item {resultItem} is add in inventory");
        }
        else
            Debug.Log("need more ingredient item");
    }
}
