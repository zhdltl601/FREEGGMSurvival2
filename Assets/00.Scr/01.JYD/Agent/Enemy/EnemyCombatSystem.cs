using UnityEngine;

public class EnemyCombatSystem : MonoBehaviour , IAgentComponent
{
    private Enemy enemy;
    
  
    public void Initialize(Agent agent)
    {
        enemy = agent as Enemy;;
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
