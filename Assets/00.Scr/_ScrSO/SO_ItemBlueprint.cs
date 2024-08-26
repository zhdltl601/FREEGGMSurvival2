using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/SO_Blueprint")]
public class SO_ItemBlueprint : ScriptableObject
{
    [SerializeField] private List<ItemStruct> list;
    [SerializeField] private List<ItemStruct> result;


    /// <summary>
    /// use GetElement2 insted;
    /// </summary>
    [Obsolete] public List<ItemStruct> GetElements => list;
    public IReadOnlyList<ItemStruct> GetElements2 => list;

    /// <summary>
    /// use GetResult2 insted
    /// </summary>
    [Obsolete] public List<ItemStruct> GetResult => result;
    public IReadOnlyList<ItemStruct> GetResult2 => result;
}
//[Serializable]
//public struct SudoDictionary<TKey, TValue>
//{
//    public TKey key;
//    public TValue value;
//}