using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// actual prefab that player holds
/// </summary>
public abstract class Item : MonoBehaviour
{
    [SerializeField] private SO_Item _so_item;
    [Header("Animation")]
    private const string inspectID = "inspect";
    public static readonly int inspectHash = Animator.StringToHash(inspectID);

    public SO_Item So_item => _so_item;
    protected virtual void Awake()
    {
    }
    public virtual void Initialize(Player player)
    {

    }
    public void Inspect()
    {
        
    }
    public virtual void OnNormalInteraction()
    {
    }

    public virtual void OnSpecialInteraction()
    {
    }
}
