using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool _isOpen = false;
    public void OnPlayerInteract(Player player)
    {
        _isOpen = !_isOpen;
        ToggleDoor();
        void ToggleDoor() 
        {
            if (_isOpen) Open();
            else Close();
            void Open()
            {
                Debug.Log("door opens");
            }
            void Close()
            {
                Debug.Log("door closes");
            }
        }

    }
}
