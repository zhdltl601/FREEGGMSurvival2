using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    [SerializeField] private Quest[] _mainQuest;
    private int _currentIndex = 0;
    public Quest GetCurrentQuest => _mainQuest[_currentIndex];

}