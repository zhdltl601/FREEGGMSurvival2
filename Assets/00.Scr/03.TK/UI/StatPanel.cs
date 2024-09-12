using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatPanel : MonoBehaviour, IPointerDownHandler
{
    private CanvasGroup statPanel;
    [SerializeField] private float fadeTime;
    private bool isOpened = false;

    private void Start()
    {
        statPanel = GetComponent<CanvasGroup>();
        SetStatPanelActive(false);
    }

    private void Update() // for Test
    {
        if (Input.GetKeyDown(KeyCode.E))
            SetStatPanelActive(!isOpened);
    }

    public void SetStatPanelActive(bool isActive)
    {
        if(isActive == true)
        {
            statPanel.DOKill();
            statPanel.DOFade(1, fadeTime);
        }
        else if(isActive == false)
        {
            statPanel.DOKill();
            statPanel.DOFade(0, fadeTime);
        }
        isOpened = isActive;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetStatPanelActive(!isOpened);
    }
}
