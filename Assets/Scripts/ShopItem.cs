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
    public string valueSuffix;
    public TextMeshProUGUI valueSuffixText;

    [Header("Upgrade Costs & Improvement")]
    public TextMeshProUGUI relativeChangeText;
    public TextMeshProUGUI costs;
    private Upgrade upgrade;

    void Start()
    {
        if (!TryGetComponent<Upgrade>(out upgrade))
        {
            Debug.Log("'Upgrade' component is missing!");
            return;
        }

        itemNameText.text = upgrade.upgradeName;
        itemDescriptionText.text = upgrade.description;
        UpdateValues();
    }

    public void UpgradeItem()
    {
        if (upgrade.TryLevelUp())
        {
            UpdateValues();
        }
    }


    private void UpdateValues()
    {
        // level
        currentLevelText.text = upgrade.level.ToString();
        maximumLevelText.text = upgrade.maxLevel.ToString();

        // relative improvement when level up
        int change = Mathf.FloorToInt(upgrade.RelativeChangeWhenLevelUp());
        relativeChangeText.text = !upgrade.MaxLevelReached()
            ? ((change > 0 ? "+" + change.ToString() : change.ToString()) + "%")
            : "";

        // costs
        costs.text = upgrade.GetUpgradeCosts().ToString() + " $";

        // current value
        valueText.text = (Mathf.Round(upgrade.GetValue() * 10) / 10).ToString();
        valueSuffixText.text = valueSuffix;
    }

}
