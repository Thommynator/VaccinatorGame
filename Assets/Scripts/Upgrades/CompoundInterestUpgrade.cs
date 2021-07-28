using UnityEngine;

/**
Compund Interest in German = Zinseszins Effekt
*/
public class CompoundInterestUpgrade : Upgrade
{
    public override float GetValue(int givenLevel)
    {
        return givenLevel == 0 ? startValue : startValue * Mathf.Pow(1 + factor, givenLevel);
    }
    public override float GetValue()
    {
        return GetValue(level);
    }
}