using UnityEngine;

public class Reward : MonoBehaviour
{
    public int rewardAmount;

    private void OnDestroy()
    {
        GameEvents.current.IncreaseMoney(rewardAmount);
    }
}
