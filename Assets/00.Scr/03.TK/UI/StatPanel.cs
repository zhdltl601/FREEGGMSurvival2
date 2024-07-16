using DG.Tweening;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    private CanvasGroup statPanel;
    [SerializeField] private float fadeTime;

    private void Start()
    {
        statPanel = GetComponent<CanvasGroup>();
        CloseStatPanel();
    }

    private void Update() // for Test
    {
        if (Input.GetKeyDown(KeyCode.E))
            OpenStatPanel();
    }

    public void OpenStatPanel()
    {
        statPanel.DOKill();
        statPanel.DOFade(1, fadeTime);
    }

    public void CloseStatPanel()
    {
        statPanel.DOKill();
        statPanel.DOFade(0, fadeTime);
    }
}
