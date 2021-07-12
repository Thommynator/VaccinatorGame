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
            level++;
            return true;
        }
        return false;
    }

    public float GetUpgradeCosts()
    {
        return (level + 1) * costsPerLevel;
    }

    public float RelativeChangeWhenLevelUp()
    {
        float currentValue = GetValue(level);
        float newValue = GetValue(level + 1);
        float difference = newValue - currentValue;
        return difference / currentValue * 100;
    }

    public bool IsActive()
    {
        return level > 0;
    }

    public bool CanLevelUp()
    {
        return level < maxLevel;
    }
}