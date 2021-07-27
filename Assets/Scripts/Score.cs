using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI attackerCountText;

    private float money;
    private float timeInSeconds;
    private int attackerCount;

    void Start()
    {
        GameEvents.current.onIncreaseMoney += IncreaseMoney;
        GameEvents.current.onIncreaseAttackerCount += IncreaseAttackerCount;
        GameEvents.current.onDecreaseAttackerCount += DecreaseAttackerCount;

        money = 0;
        UpdateMoneyScoreVisuals();

        timeInSeconds = Time.realtimeSinceStartup;
        StartCoroutine(IncreaseTimer());

        attackerCount = 0;
    }

    private void IncreaseMoney(float increase)
    {
        money += increase;
        UpdateMoneyScoreVisuals();
    }

    private void UpdateMoneyScoreVisuals()
    {
        moneyText.text = money.ToString() + " $";
    }

    private IEnumerator IncreaseTimer()
    {
        while (true)
        {
            timeInSeconds = Time.realtimeSinceStartup;

            TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
            // 0:00 --> first number = index, second number = formatting (https://tinyurl.com/3fd6w7ey)
            timeText.text = string.Format("{0:00}:{1:00}:{2:0}", timeSpan.TotalMinutes, timeSpan.Seconds, timeSpan.Milliseconds / 100);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void IncreaseAttackerCount()
    {
        attackerCount += 1;
        UpdateAttackerCountVisuals();
    }

    private void DecreaseAttackerCount()
    {
        attackerCount -= 1;
        Mathf.Max(attackerCount, 0);
        UpdateAttackerCountVisuals();
    }

    private void UpdateAttackerCountVisuals()
    {
        attackerCountText.text = attackerCount.ToString();
    }

    private void OnDestroy()
    {
        GameEvents.current.onIncreaseMoney -= IncreaseMoney;
    }
}