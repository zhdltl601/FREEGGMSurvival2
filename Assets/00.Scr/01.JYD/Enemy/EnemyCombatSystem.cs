using UnityEngine;

public class EnemyCombatSystem : MonoBehaviour , IEnmyComponent
{
    private Enemy enemy;
    public void Initialize(Enemy _enemy)
    {
        enemy = _enemy;
    }

    public bool IsAttackAble()
    {
        Vector3 targetPos = enemy.targetTrm.position;
        return Vector3.Distance(targetPos, transform.position) < enemy.attackAbleRange;
    }

    public void Attack()
    {
        
    }
    
    
}
