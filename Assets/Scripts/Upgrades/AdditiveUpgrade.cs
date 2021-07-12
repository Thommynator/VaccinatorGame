public class AdditiveUpgrade : Upgrade
{
    public override float GetValue(int givenLevel)
    {
        return givenLevel == 0 ? startValue : startValue + givenLevel * factor;
    }
    public override float GetValue()
    {
        return GetValue(level);
    }
}