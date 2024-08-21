using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/SO_Blueprint")]
public class SO_ItemBlueprint : ScriptableObject
{
    [SerializeField] private List<ItemStruct> list;
    [SerializeField] private List<ItemStruct> result;
    public List<ItemStruct> GetElements => list;
    public List<ItemStruct> GetResult => result;
}   
