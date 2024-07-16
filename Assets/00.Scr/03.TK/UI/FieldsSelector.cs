using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldsSelector : MonoBehaviour
{
    private BaseField[] fields;

    private void Start()
    {
        fields = GetComponentsInChildren<BaseField>();
    }

    public void CancleSelectField()
    {
        foreach(var field in fields)
        {
            field.CloseField();
        }
    }
}
