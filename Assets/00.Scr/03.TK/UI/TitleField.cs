using DG.Tweening;
using UnityEngine;

public class TitleField : BaseField
{
    [SerializeField] private float fadeTime;

    public override void CloseField()
    {
        CanvasGroupComponent.DOKill();
        CanvasGroupComponent.DOFade(0, fadeTime);
    }

    public override void OpenField()
    {
        CanvasGroupComponent.DOKill();
        CanvasGroupComponent.DOFade(1, fadeTime);
    }
}
