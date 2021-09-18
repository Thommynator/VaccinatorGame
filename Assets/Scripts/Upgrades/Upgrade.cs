using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    [Header("Text")]
    public string upgradeName;
    public string description;

    [Header("Upgrade Stats")]
    public float startValue;
    public int level;
    public float factor;
    public int maxLevel;
    public float costsPerLevel;

    public abstract float GetValue();
    public abstract float GetValue(int level);

    public bool TryLevelUp()
    {
        if (CanLevelUp())
        {
            GameEvents.current.DecreaseMoney(GetUpgradeCosts());
            level++;
            return true;
        }
        return false;
    }

    public int GetUpgradeCosts()
    {
        return Mathf.RoundToInt((level + 1) * costsPerLevel);
    }

    public float RelativeChangeWhenLevelUp()
    {
        float currentValue = GetValue(level);
        float newValue = GetValue(level + 1);
        float difference = newValue - currentValue;
        return difference / currentValue * 100;
    }

    public bool CanLevelUp()
    {
        return !MaxLevelReached() && CanBuyUpgrade();
    }

    public bool MaxLevelReached()
    {
        return level >= maxLevel;
    }

    public bool CanBuyUpgrade()
    {
        return GameManager.current.GetMoney() >= GetUpgradeCosts();
    }
}