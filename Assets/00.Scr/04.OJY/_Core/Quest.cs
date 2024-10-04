using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Quest
{
    private const string questPrefix = "Current Quest :";
    [SerializeField] private string _name;
    [SerializeField] private QuestCondition _questCondition;

    public string GetName => $"{questPrefix} {_name}";
    public QuestCondition GetCondition => _questCondition;
}