using System.Collections.Generic;
using UnityEngine;
//#define DEBUG_SO
[CreateAssetMenu(menuName = "SO/SO_Item")]
public class SO_Item : ScriptableObject
{
    [Header("General")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _maxAmount;
    [SerializeField] private string _name;

    public Sprite GetIcon => _icon;
    public string GetName => _name;
    public int GetMaxAmount => _maxAmount;

    [Header("InventoryVisual")]
    [SerializeField] private List<Vector3> _posVisInv;
    [SerializeField] private List<Vector3> _rotVisInv;
    [SerializeField] private List<GameObject> _models;
    public IReadOnlyList<Vector3> GetVisPosInv => _posVisInv;

    public GameObject GetPrefab
    {
        get
        {
            if(_models.Count > 1)
            {
                int r = Random.Range(0, _models.Count);
                return _models[r];
            }
            return _models[0];
        }
    }
    public IReadOnlyList<Vector3> GetRotation => _rotVisInv;
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
