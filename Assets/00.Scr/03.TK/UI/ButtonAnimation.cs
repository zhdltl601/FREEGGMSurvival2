using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;

    [SerializeField] private float spentTimeToScaleUp;
    [SerializeField] private float spentTimeToScaleDown;
    [SerializeField] private float targetScaleMultiplier;
    private Vector3 originScale;
    private Vector3 addedScale;

    private bool isScaleIncreasing = false;

    private void Start()
    {
        rectTransform = GetComponentInChildren<RectTransform>();

        originScale = Vector3.one;
        addedScale = originScale * targetScaleMultiplier;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isScaleIncreasing)
        {
            rectTransform.DOKill();
            rectTransform.DOScale(addedScale, spentTimeToScaleUp);
            isScaleIncreasing = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isScaleIncreasing)
        {
            rectTransform.DOKill();
            rectTransform.DOScale(originScale, spentTimeToScaleDown);
            isScaleIncreasing = false;
        }
    }
}
