using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private GameObject      _questPanel;
    [SerializeField] private TextMeshProUGUI _questText;
    private uint day;

    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private TextMeshProUGUI _nightText;
    
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
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
    
    [ContextMenu("Test-day")]
    private void OnDayUI()
    {
        day++;
        //print("³·UI");
        _dayText.SetText($"{day}th morning.");
        _dayText.gameObject.SetActive(true);
        
        _dayText.DOFade(1 , 1).OnComplete(() =>
        {
            _dayText.DOFade(0, 0.4f).OnComplete(() =>
            {
                _dayText.gameObject.SetActive(false);
            });
        });
    }
    
    [ContextMenu("Test-night")]
    private void OnNightUI()
    {
        //print("¹ãUI");
        _nightText.SetText($"{day}th night.");
        _nightText.gameObject.SetActive(true);
        
        _nightText.DOFade(1 , 1).OnComplete(() =>
        {
            _nightText.DOFade(0, 0.4f).OnComplete(() =>
            {
                _nightText.gameObject.SetActive(false);
            });
        });
        
        
    }
    public void ToggleQuest()
    {
        var curQueust = QuestManager.Instance.GetCurrentQuest;
        bool val = !_questPanel.activeSelf;
        _questPanel.SetActive(val);
        _questText.text = curQueust.GetName;
    }
}
