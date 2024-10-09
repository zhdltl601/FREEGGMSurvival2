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
        Instantiate(_drops[i],transform.position+Vector3.up + Vector3.right*Random.Range(-0.5f,0.5f)+ Vector3.forward * Random.Range(-0.5f, 0.5f),Quaternion.identity);
    }
}
