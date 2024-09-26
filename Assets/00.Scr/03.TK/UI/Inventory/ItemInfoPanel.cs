using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public static ItemInfoPanel Instance;
    private CanvasGroup cG;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI count;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance ??= this;
        cG = GetComponent<CanvasGroup>();
    }

    public void SetItemInfoPanel(Sprite icon, string itemName, string itemCnt)
    {
        cG.alpha = 1;
        this.icon.sprite = icon;
        this.itemName.text = itemName;
        this.count.text = itemCnt;
    }

    public void DisableInfoPanel()
    {
        icon.sprite = null;
        itemName.text = "";
        count.text = "";
        cG.alpha = 0;
    }
}
