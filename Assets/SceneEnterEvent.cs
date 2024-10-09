using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnterEvent : MonoBehaviour
{
    private void Awake()
    {
        BshAmiKlr.GameManager.Instance.f.transform.position = transform.position;
    }
}
