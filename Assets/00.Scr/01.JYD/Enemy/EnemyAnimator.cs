using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour,IEnmyComponent
{
    private readonly int RecoveryIndex = Animator.StringToHash("RecoveryIndex");
    
    private Enemy enemy;
    public Animator animator { get; private set; }

    public void Initialize(Enemy _enemy)
    {
        enemy = _enemy;
        animator = GetComponent<Animator>();
    }

    private void AnimationEnd()
    {
        enemy.AnimationEnd();
    }

    public void SetupRecovery(int index)
    {
        animator.SetInteger(RecoveryIndex,index);
    }
}
