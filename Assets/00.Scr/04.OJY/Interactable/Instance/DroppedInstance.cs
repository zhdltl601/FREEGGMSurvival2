using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedInstance : MonoBehaviour, IInteractable
{
    [SerializeField] private SO_Item soitem;
    public void OnPlayerInteract(Player player)
    {
        InventoryUI.Instance.PlayerInventory.TryAddItemToInventory(soitem);
    }

}
