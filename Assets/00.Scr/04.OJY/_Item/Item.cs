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
    public SO_Item So_item => _so_item;
}
