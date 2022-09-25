using UnityEngine;

public class ItemUpgrade : MonoBehaviour
{

    [Header("Text")]
    public string upgradeName;

    public string description;

    [Header("Upgrade Stats")]
    public AnimationCurve costsPerLevel;

    public float startValue;

    public float improvementPerLevel;

    public int level;


    void Start()
    {
        level = 0;
    }

    public float GetValue()
    {
        return startValue + improvementPerLevel * level;
    }

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

    public bool CanLevelUp()
    {
        return !MaxLevelReached() && CanBuyUpgrade();
    }

    public int getMaxLevel()
    {
        Keyframe lastKey = costsPerLevel.keys[costsPerLevel.keys.Length - 1];
        return Mathf.RoundToInt(lastKey.time);
    }

    public bool MaxLevelReached()
    {

        return level >= getMaxLevel();
    }

    public bool CanBuyUpgrade()
    {
        return GameManager.current.GetMoney() >= GetUpgradeCosts();
    }

    public int GetUpgradeCosts()
    {
        return Mathf.RoundToInt(costsPerLevel.Evaluate(level));
    }

}
