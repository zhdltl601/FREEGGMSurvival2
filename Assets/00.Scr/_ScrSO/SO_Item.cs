using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#define DEBUG_SO
[CreateAssetMenu(menuName = "SO/SO_Item")]
public class SO_Item : ScriptableObject
{
    [Header("General")]
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private int _maxAmount;
    public int GetMaxAmount => _maxAmount;

    [Header("InventoryVisual")]
    [SerializeField] private List<Vector3> _posVisInv;
    [SerializeField] private List<Vector3> _posVisCraft;
    [SerializeField] private List<GameObject> _models;
    public int GetPosMaxCount => _posVisInv.Count;

    /// <summary>
    /// sdadda
    /// </summary>
    [Obsolete] public List<Vector3> GetPosInv => _posVisInv;
    /// <summary>
    /// awdadwadadad
    /// </summary>
    [Obsolete] public List<Vector3> GetPosCraft => _posVisCraft;
    public IReadOnlyList<Vector3> GetPosInv2 => _posVisInv;
    public IReadOnlyList<Vector3> GetPosCraft2 => _posVisCraft;

    public GameObject GetPrefab
    {
        get
        {
            if(_models.Count > 1)
            {
                int r = UnityEngine.Random.Range(0, _models.Count);
                return _models[r];
            }
            return _models[0];
        }
    }
#if DEBUG_SO
    private void OnEnable()
    {
        if(_pos.Count != _maxAmount)
        {
            Debug.LogError("pos amount is not maxAmount");
        }
    }
#endif
}
