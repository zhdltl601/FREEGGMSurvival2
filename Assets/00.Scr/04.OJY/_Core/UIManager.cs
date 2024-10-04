using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private GameObject      _questPanel;
    [SerializeField] private TextMeshProUGUI _questText;
    private uint day;
    private void Start()
    {
        DayManager.OnChangeState += HandleOnStateChange;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        DayManager.OnChangeState -= HandleOnStateChange;
    }
    private void HandleOnStateChange(EDayState obj)
    {
        switch (obj)
        {
            case EDayState.Morning:
                OnDayUI();
                break;
            case EDayState.Night:
                OnNightUI();
                break;
        }
    }
    private void OnDayUI()
    {
        day++;
        print("≥∑UI");
    }
    private void OnNightUI()
    {
        print("π„UI");
    }
    public void ToggleQuest()
    {
        var curQueust = QuestManager.Instance.GetCurrentQuest;
        bool val = !_questPanel.activeSelf;
        _questPanel.SetActive(val);
        _questText.text = curQueust.GetName;
    }
}
