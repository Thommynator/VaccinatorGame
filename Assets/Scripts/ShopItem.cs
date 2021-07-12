using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{

    [Header("Text Elements")]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI maximumLevelText;
    public TextMeshProUGUI relativeChangeText;
    public TextMeshProUGUI costs;
    private Upgrade upgrade;

    void Start()
    {
        if (!TryGetComponent<Upgrade>(out upgrade))
        {
            Debug.Log("'Upgrade' component is missing!");
        }

        itemNameText.text = upgrade.upgradeName;
        itemDescriptionText.text = upgrade.description;
        UpdateValues();
    }

    public void UpgradeItem()
    {
        bool status = upgrade.TryLevelUp();
        Debug.Log(status ? "Level up" : "Maximum reached");
        UpdateValues();
    }


    private void UpdateValues()
    {
        currentLevelText.text = upgrade.level.ToString();
        maximumLevelText.text = upgrade.maxLevel.ToString();
        int change = Mathf.FloorToInt(upgrade.RelativeChangeWhenLevelUp());
        relativeChangeText.text = upgrade.CanLevelUp()
            ? ((change > 0 ? "+" + change.ToString() : change.ToString()) + "%")
            : "";
        costs.text = upgrade.GetUpgradeCosts().ToString() + " $";
    }
}
