using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    private EDayState _currentState;
    private void Start()
    {
        DayManager.OnChangeState += HandleOnChange;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        DayManager.OnChangeState -= HandleOnChange;
    }
    private void HandleOnChange(EDayState obj)
    {
        _currentState = obj;
    }
    private void Update()
    {
        if (_currentState == EDayState.Morning)
        {

        }
    }
}
