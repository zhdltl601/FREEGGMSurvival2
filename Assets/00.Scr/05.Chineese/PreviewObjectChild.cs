using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObjectChild : MonoBehaviour
{
    public bool isStuck = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("엔터밍");
        if (other.CompareTag("Building"))
        {
            isStuck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("엑시트밍");
        if (other.CompareTag("Building"))
        {
            Debug.Log("!!!!!!!!");
            isStuck = false;
        }
    }

    public bool IsStuck()
    {
        Debug.Log(isStuck);
        return isStuck;
    }
}
