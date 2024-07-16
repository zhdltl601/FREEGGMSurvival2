using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatPointText : MonoBehaviour
{
    private static TextMeshProUGUI pointAmountText;

    public static int currentPoint;

    void Start()
    {
        pointAmountText = GetComponent<TextMeshProUGUI>();

        GetCurrentStatPoint();
    }

    private void GetCurrentStatPoint()
    {
        string tempText = pointAmountText.text;
        currentPoint = int.Parse(tempText);
    }

    public static void ChangeValueNotify()
    {
        pointAmountText.text = currentPoint.ToString();
    }
}
