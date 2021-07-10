using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI moneyText;

    private float money;
    private float timeInSeconds;

    void Start()
    {
        GameEvents.current.onIncreaseMoney += IncreaseMoney;

        money = 0;
        UpdateMoneyScoreVisuals();

        timeInSeconds = Time.realtimeSinceStartup;
        StartCoroutine(IncreaseTimer());
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

    private void OnDestroy()
    {
        GameEvents.current.onIncreaseMoney -= IncreaseMoney;
    }
}
