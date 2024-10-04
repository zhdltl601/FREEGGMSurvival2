using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
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
        print("≥∑UI");
    }
    private void OnNightUI()
    {
        print("π„UI");
    }
}
