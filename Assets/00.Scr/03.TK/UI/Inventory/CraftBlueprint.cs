using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftBlueprint : MonoBehaviour
{
    [SerializeField] private Image ingredientIcon;
    [SerializeField] private TextMeshProUGUI ingredientCount;
    [SerializeField] private TextMeshProUGUI resultItemName;

    public void SetUI(Image icon, string cnt, string resultName)
    {
        ingredientIcon = icon;
        ingredientCount.text = cnt;
        resultItemName.text = resultName;
    }
}
