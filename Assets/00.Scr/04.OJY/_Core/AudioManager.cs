using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
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
                print("Morning");
                break;
            case EDayState.Night:
                print("Night");
                break;

        }
    }
}
