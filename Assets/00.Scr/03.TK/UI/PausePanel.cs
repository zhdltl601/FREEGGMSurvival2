using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private float backSize;
    [SerializeField] private RectTransform pausePanel;

    private bool isPause = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPause)
            {
                pausePanel.DOKill();
                pausePanel.DOMoveX(backSize, 0.3f);
                isPause = false;
            }
            else
            {
                pausePanel.DOKill();
                pausePanel.DOMoveX(0, 0.3f);
                isPause = true;
            }
        }
    }
}
