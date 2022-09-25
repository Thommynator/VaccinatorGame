using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{

    [Header("Item Description")]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

    [Header("Level Info")]
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI maximumLevelText;

    [Header("Value")]

    public TextMeshProUGUI valueText;
    public TextMeshProUGUI improvementText;
    public string valueSuffix;
    public TextMeshProUGUI valueSuffixText;

    [Header("Upgrade Costs")]
    public TextMeshProUGUI costs;
    private ItemUpgrade itemUpgrade;

    void Start()
    {
        if (!TryGetComponent<ItemUpgrade>(out itemUpgrade))
        {
            Debug.Log("'Upgrade' component is missing!");
            return;
        }

        itemNameText.text = itemUpgrade.upgradeName;
        itemDescriptionText.text = itemUpgrade.description;
        UpdateValues();
    }

    public void LevelUp()
    {
        if (itemUpgrade.TryLevelUp())
        {
            UpdateValues();
        }
    }


    private void UpdateValues()
    {
        // level
        currentLevelText.text = itemUpgrade.level.ToString();
        maximumLevelText.text = itemUpgrade.getMaxLevel().ToString();

        // costs
        costs.text = itemUpgrade.GetUpgradeCosts().ToString() + " $";

        // current value
        valueText.text = (Mathf.Round(itemUpgrade.GetValue() * 100) / 100).ToString();
        float improvement = itemUpgrade.improvementPerLevel;
        improvementText.text = improvement >= 0 ? "+" + improvement.ToString() : improvement.ToString();
        valueSuffixText.text = valueSuffix;
    }

}
