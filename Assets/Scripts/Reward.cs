using UnityEngine;

public class Reward : MonoBehaviour
{
    public float rewardAmount;

    private void OnDestroy()
    {
        GameEvents.current.IncreaseMoney(rewardAmount);
    }
}
