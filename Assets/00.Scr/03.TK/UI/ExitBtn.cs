using UnityEngine;
using UnityEngine.UI;

public class ExitBtn : MonoBehaviour
{
    private RectTransform rtrm;
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        rtrm = transform.root.GetComponentInParent<RectTransform>();
    }

    private void OnEnable()
    {
        btn?.onClick.AddListener(ExitBtnClick);
    }

    private void OnDestroy()
    {
        btn?.onClick.RemoveListener(ExitBtnClick);
    }

    public void ExitBtnClick()
    {
        rtrm.gameObject.SetActive(false);
    }
}
