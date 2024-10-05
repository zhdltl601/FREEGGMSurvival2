using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMom : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        transform.SetParent(null);
    }
}
