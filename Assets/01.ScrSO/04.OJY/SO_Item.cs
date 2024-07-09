using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/SO_Item")]
public class SO_Item : ScriptableObject
{
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private int maxAmount;
    public int GetMaxAmount => maxAmount;
    
}
