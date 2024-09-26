using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipLineAnchor : MonoBehaviour,IInteracivable
{
    public LineRenderer lineRenderer;

    public void GetInteractive()
    {
        GameManager.instance.player.playerMovement.OnZipline?.Invoke();
    }
}
