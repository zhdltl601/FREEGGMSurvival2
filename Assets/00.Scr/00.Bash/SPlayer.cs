using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayer : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.player = this;
    }

    public PlayerMovement playerMovement;
    public ZipLineMana zipLine;
}
