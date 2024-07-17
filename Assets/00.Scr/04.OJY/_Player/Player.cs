using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        DebugUI.Instance.ToggleInventory();

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            DebugUI.Instance.ToggleInventory();
        }
    }
}
