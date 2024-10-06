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
    private BlueprintViewer bpViewer;

    public bool canCraft = false;

    private void Start()
    {
        bpViewer = BlueprintViewer.Instance;
        inventory = FindObjectOfType<Inventory>();
    }

    public void SetUI(SO_ItemBlueprint itemInfo)
    {
        IngrediantSetting(itemInfo);

        resultItem = itemInfo.GetResult[0].so_item;

        resultItemIcon.sprite = itemInfo.GetResult[0].so_item.GetIcon;
        resultItemName.text = itemInfo.GetResult[0].so_item.GetName;
        resultItemAmount.text = itemInfo.GetResult[0].amount.ToString();
    }

    private void IngrediantSetting(SO_ItemBlueprint itemInfo)
    {
        #region Item count setting
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
        #endregion

        StringBuilder sb = new StringBuilder();

        bool canComb = true;

        int idx = 0;
        foreach(var item in itemInfo.GetElement)
        {
            sb.Clear();
            int currentAmount = 0;
            if (bpViewer.currentItemSetting.ContainsKey(item.so_item.GetName))
            {
                currentAmount = bpViewer.currentItemSetting[item.so_item.GetName];
            }
            sb.Append(currentAmount.ToString());
            sb.Append("/");
            int needAmount = item.amount;
            sb.Append(needAmount);
            if (needAmount != 0)
                ingredientAmount[idx].text = sb.ToString();
            else
                ingredientAmount[idx].text = "";

            if (currentAmount < needAmount)
                canComb = false;
        }

        canCraft = canComb;
    }

    public void OnPointerDown(PointerEventData eventData)
    { 
        if (canCraft)
        {
            inventory.TryAddItemToInventory(resultItem);
            BlueprintViewer.Instance.RemoveAllByCrafting();
            Debug.Log($"item {resultItem} is add in inventory");
        }
        else
            Debug.Log("need more ingredient item");
    }
}
