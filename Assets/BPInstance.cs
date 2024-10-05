using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BPInstance : MonoBehaviour
{
    [SerializeField] private SO_ItemBlueprint bpTarget;
    private void OnTriggerEnter(Collider other)
    {
        Inventory.AddBluePrint(bpTarget);
        var col = GetComponent<Collider>();
        col.enabled = false;
        Destroy(gameObject, 1);
    }
}
