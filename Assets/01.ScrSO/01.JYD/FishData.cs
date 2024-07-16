using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/Fish")]
public class FishData : ScriptableObject
{
    public string fishName;

    public GameObject fishObj;

    public float fishFlipInterval;
    
    public float catchTime;
    
    public float minMoveSpeed;
    public float maxMoveSpeed;
    
}
