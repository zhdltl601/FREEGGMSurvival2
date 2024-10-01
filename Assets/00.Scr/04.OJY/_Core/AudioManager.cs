using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioClip audMorning;
    [SerializeField] private AudioClip audNight;
    private AudioSource audSrc2D;
    private void Start()
    {
        audSrc2D = GetComponent<AudioSource>();
        audSrc2D.loop = false;
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
        audSrc2D.clip = audNight;
        audSrc2D.Play();
    }

    private void OnMorning()
    {
        print("Morning");
        audSrc2D.clip = audMorning;
        audSrc2D.Play();
    }
}
