using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _drops = new List<GameObject>();

    private void OnEnable()
    {
        GetComponent<ExHealth>().OnDeadEvent += DropItems;
    }
    void DropItems()
    {
        for(int i = 0; i < _drops.Count; i++)
        Instantiate(_drops[0],transform.position,Quaternion.identity);
    }
}
