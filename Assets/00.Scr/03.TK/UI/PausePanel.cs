using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private float backSize;
    [SerializeField] private RectTransform pausePanel;
    
    private bool isPause = false;

    private void Start()
    {
        ActivePausePanel(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ActivePausePanel(!isPause);
        }
    }

    public void ActivePausePanel(bool isActive)
    {
        if (isActive)
        {
            pausePanel.gameObject.SetActive(true);
            pausePanel.DOKill();
            pausePanel.DOMoveX(0, 0.3f);
        }
        else if(!isActive)
        {
            pausePanel.DOKill();
            pausePanel.DOMoveX(backSize, 0.3f)
                .OnComplete(() => pausePanel.gameObject.SetActive(false));
        }

        isPause = isActive;
    }
}
