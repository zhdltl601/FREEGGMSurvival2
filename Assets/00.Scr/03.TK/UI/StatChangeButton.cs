using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatChangeButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statAmountText;

    string currentAmount;
    
    public void StatIncrease()
    {
        if (StatPointText.currentPoint <= 0) return;

        currentAmount = statAmountText.text;
        int.TryParse(currentAmount, out int result);

        result++;
        currentAmount = result.ToString();

        statAmountText.text = currentAmount;

        StatPointText.currentPoint--;
        StatPointText.ChangeValueNotify();
    }

    public void StatDecrease()
    {
        currentAmount = statAmountText.text;
        int.TryParse(currentAmount, out int result);

        if(result <= 1) return;

        result--;
        currentAmount = result.ToString();

        statAmountText.text = currentAmount;

        StatPointText.currentPoint++;
        StatPointText.ChangeValueNotify();
    }
}
