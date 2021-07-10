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
        money = 0;
        timeInSeconds = Time.realtimeSinceStartup;

        StartCoroutine(IncreaseTimer());
    }

    // Update is called once per frame
    void Update()
    {

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
}
