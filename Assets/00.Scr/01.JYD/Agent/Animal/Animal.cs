using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum AnimalStateEnum
{
    Idle,
    Move,
    Sleep,
    Eat,
    Hostile,
}

public class Animal : Agent
{
    private AnimalStateMachine StateMachine;
    
    public bool isDead;

    private void Awake()
    {
        base.Awake();
        
        StateMachine = new AnimalStateMachine();
        StateMachine.AddState(AnimalStateEnum.Idle , new AnimalIdleState(this , StateMachine , "Idle"));
        StateMachine.AddState(AnimalStateEnum.Move , new AnimalMoveState(this , StateMachine , "Move"));

    }

    private void Start()
    {
        StateMachine.Initialize(AnimalStateEnum.Idle);
    }

    private void Update()
    {
        StateMachine.currentState.Update();
    }
    
    public void AnimationEnd()
    {
        StateMachine.currentState.AnimationTrigger();
    }
}
