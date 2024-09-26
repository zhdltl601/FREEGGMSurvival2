using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftBlueprint : MonoBehaviour
{
    [SerializeField] private Image ingredientIcon;
    [SerializeField] private TextMeshProUGUI resultItemName;
    [SerializeField] private TextMeshProUGUI amountCount;

    public void SetUI(Sprite icon, string resultName, string amount)
    {
        ingredientIcon.sprite = icon;
        resultItemName.text = resultName;
        amountCount.text = amount;
    }
}
