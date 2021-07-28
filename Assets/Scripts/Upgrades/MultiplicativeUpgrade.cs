public class MultiplicativeUpgrade : Upgrade
{
    public override float GetValue(int givenLevel)
    {
        return givenLevel == 0 ? startValue : startValue + startValue * givenLevel * factor;
    }
    public override float GetValue()
    {
        return GetValue(level);
    }

}