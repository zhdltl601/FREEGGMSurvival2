using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoSingleton<ItemInfoPanel>
{
    private CanvasGroup cG;
    private RectTransform rtrm;
    [SerializeField] private RectTransform canvasRtrm;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI count;

    protected override void Awake()
    {
        base.Awake();
        rtrm = GetComponent<RectTransform>();
        cG = GetComponent<CanvasGroup>();
    }

    public void SetItemInfoPanel(Sprite icon, string itemName, int itemCnt, Vector2 screenPosition, Camera camInv)//, Vector3 canvasPos)
    {
        cG.alpha = 1;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRtrm, screenPosition,camInv, out Vector2 localPos);

        this.icon.sprite = icon;
        this.itemName.text = itemName;
        this.count.text = itemCnt.ToString();
        rtrm.localPosition = localPos;
        //transform.position = canvasPos;
    }

    public void DisableInfoPanel()
    {
        icon.sprite = null;
        itemName.text = "";
        count.text = "";
        cG.alpha = 0;
    }
}
