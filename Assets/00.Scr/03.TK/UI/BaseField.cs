using UnityEngine;

public abstract class BaseField : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroupComponent => canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public abstract void OpenField();

    public abstract void CloseField();
}
