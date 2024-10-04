using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoSingleton<ItemInfoPanel>
{
    private CanvasGroup cG;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI count;

    protected override void Awake()
    {
        base.Awake();
        cG = GetComponent<CanvasGroup>();
    }

    public void SetItemInfoPanel(Sprite icon, string itemName, int itemCnt)//, Vector3 canvasPos)
    {
        cG.alpha = 1;
        this.icon.sprite = icon;
        this.itemName.text = itemName;
        this.count.text = itemCnt.ToString();
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
