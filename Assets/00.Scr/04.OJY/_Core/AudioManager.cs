using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioClip audMorning;
    [SerializeField] private AudioClip audNight;
    private AudioSource audSource;
    private void Start()
    {
        audSource = GetComponent<AudioSource>();
        audSource.loop = false;
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
                OnMorning();
                break;
            case EDayState.Night:
                OnNight();
                break;

        }
    }

    private void OnNight()
    {
        print("Night");
        audSource.clip = audNight;
        audSource.Play();
    }

    private void OnMorning()
    {
        print("Morning");
        audSource.clip = audMorning;
        audSource.Play();
    }
}
