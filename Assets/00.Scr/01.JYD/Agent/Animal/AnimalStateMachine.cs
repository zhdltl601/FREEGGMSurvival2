using System.Collections.Generic;
using Unity.VisualScripting;

public class AnimalStateMachine
{
    public AnimalState currentState;

    private Dictionary<AnimalStateEnum, AnimalState> animalStates = new Dictionary<AnimalStateEnum, AnimalState>();
    
    public void AddState(AnimalStateEnum animalState , AnimalState state )
    {
        animalStates.Add(animalState , state);
    }

    public void ChangeState(AnimalStateEnum enemyStateEnum)
    {
        currentState.Exit();
        currentState = animalStates[enemyStateEnum];
        currentState.Enter();
    }

    public void Initialize(AnimalStateEnum animalState)
    {
        currentState = animalStates[animalState];
        currentState.Enter();
    }
    
}