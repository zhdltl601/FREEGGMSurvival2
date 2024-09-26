using System.Collections.Generic;

public class EnemyStateMachine
{
    public EnemyState currentState;

    public Dictionary<EnemyStateEnum, EnemyState> _enemyDictionary = new Dictionary<EnemyStateEnum, EnemyState>();
    
    
    public void Initialize(EnemyStateEnum startState)
    {
        currentState = _enemyDictionary[startState];
        currentState.Enter();
    }
    
    public void ChangeState(EnemyStateEnum enemyState)
    {
        currentState.Exit();
        currentState = _enemyDictionary[enemyState];
        currentState.Enter();
    }
    
    public void AddDictionary(EnemyStateEnum enemyStateEnum, EnemyState enemyState)
    {
        _enemyDictionary.Add(enemyStateEnum , enemyState);
    }
    
    
}